using Azure.Identity;
using BlogLab.Models.Photo;
using BlogLab.Repository;
using BlogLab.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BlogLab.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IPhotoService _photoService;

        public PhotoController(IPhotoRepository photoRepository, IBlogRepository blogRepository, IPhotoService photoService)
        {
            _photoRepository = photoRepository;
            _blogRepository = blogRepository;
            _photoService = photoService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Photo>> UploadPhoto(IFormFile file)
        {
            var applicationUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            var uploadResult = await _photoService.AddPhotoAsync(file);

            if (uploadResult.Error != null)
                return BadRequest(uploadResult.Error.Message);

            var photoCreate = new PhotoCreate
            {
                ImageUrl = uploadResult.SecureUrl.AbsoluteUri,
                PublicId = uploadResult.PublicId,
                Description = file.FileName
            };

            var photo = await _photoRepository.InsertAsync(photoCreate, applicationUserId);

            return Ok(photo);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Photo>>> GetByApplicationUserId()
        {
            var applicationUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            var photos = await _photoRepository.GetAllByUserIdAsync(applicationUserId);

            return Ok(photos);
        }

        [HttpGet("{photoId}")]
        public async Task<ActionResult<Photo>> Get(int photoId)
        {
            var photo = await _photoRepository.GetAsync(photoId);
            
            return Ok(photo);
        }

        [Authorize]
        [HttpDelete("{photoId}")]
        public async Task<ActionResult<int>> Delete(int photoId)
        {
            var applicationUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            var foundPhoto = await _photoRepository.GetAsync(photoId);

            if (foundPhoto != null)
            {
                if (foundPhoto.ApplicationUserId == applicationUserId)
                {
                    var blogs = await _blogRepository.GetAllByUserIdAsync(applicationUserId);
                    
                    var usedInBlog = blogs.Any(b => b.PhotoId == photoId);
                    if (usedInBlog)
                        return BadRequest("photo is in use by a published blog.");

                    var deleteResult = await _photoService.DeletePhotoAsync(foundPhoto.PublicId);
                    if (deleteResult.Error != null)
                        return BadRequest(deleteResult.Error.Message);

                    var affectedRows = await _photoRepository.DeleteAsync(foundPhoto.PhotoId);

                    return Ok(affectedRows);
                }
                else
                {
                    return BadRequest("Photo not uploaded by current user.");
                }
            }
            
            return BadRequest("Photo not found.");
        }
    }
}
