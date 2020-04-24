using ImageGallery.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGallery.API.Authorization
{
    public class MustOwnImageHandler:AuthorizationHandler<MustOwnImageRequriement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGalleryRepository _galleryRepository;

        public MustOwnImageHandler(IHttpContextAccessor httpContextAccessor, IGalleryRepository galleryRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            this._galleryRepository = galleryRepository;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
                MustOwnImageRequriement requirement)
        {
            var imageId = _httpContextAccessor.HttpContext.GetRouteValue("id").ToString();
            if (!Guid.TryParse(imageId,out Guid imageIdGuid))
            {
                context.Fail();
                return Task.CompletedTask;
            }
            var ownerId = context.User.Claims.FirstOrDefault(o => o.Type == "sub")?.Value;
            if (!_galleryRepository.IsImageOwner(imageIdGuid,ownerId))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            //success
            context.Succeed(requirement);
            return Task.CompletedTask;

        }
    }
}
