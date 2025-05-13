using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        //        UserProfileController
        //GET /profiles/{userId
        //    }

        //    PUT /profiles/{userId
        //} – (bio, konum, doğum tarihi, sosyal linkler)

        //POST /profiles/{userId}/ avatar – profil fotoğrafı yükleme

        //POST /profiles/{userId}/ cover – kapak fotoğrafı yükleme

        //POST /profiles/{userId}/ interests – ilgi alanı ekleme/çıkarma

        //GET /profiles/{userId}/ gallery
    }
}
