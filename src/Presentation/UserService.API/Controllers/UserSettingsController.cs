using Microsoft.AspNetCore.Mvc;

namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSettingsController : ControllerBase
    {
        //        UserSettingsController
        //GET /settings/{userId
        //    }

        //    PUT /settings/{userId
        //} – tüm ayarları topluca güncelleme

        //PUT /settings/{userId}/ notifications – bildirim tercihleri

        //PUT /settings/{userId}/ privacy – gizlilik seçenekleri

        //PUT /settings/{userId}/ security – şifre, 2FA

        //PUT /settings/{userId}/ preferences – tema, dil vs.
    }
}
