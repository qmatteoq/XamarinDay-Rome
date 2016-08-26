using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using TrackSeries.Backend.DataObjects;
using TrackSeries.Backend.Models;

namespace TrackSeries.Backend.Controllers
{
    public class FavoriteShowController : TableController<FavoriteShow>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<FavoriteShow>(context, Request);
        }

        [Authorize]
        // GET tables/FavoriteShow
        public async Task<IQueryable<FavoriteShow>> GetAllFavoriteShows()
        {
            var credentials = await this.User.GetAppServiceIdentityAsync<TwitterCredentials>(this.Request);
            var result = Query().Where(x => x.UserId == credentials.UserId);
            return result;
        }

        // GET tables/FavoriteShow/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<FavoriteShow> GetFavoriteShow(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/FavoriteShow/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<FavoriteShow> PatchFavoriteShow(string id, Delta<FavoriteShow> patch)
        {
            return UpdateAsync(id, patch);
        }

        [Authorize]
        // POST tables/FavoriteShow
        public async Task<IHttpActionResult> PostFavoriteShow(FavoriteShow item)
        {
            var credentials = await this.User.GetAppServiceIdentityAsync<TwitterCredentials>(this.Request);
            item.UserId = credentials.UserId;
            FavoriteShow current = await InsertAsync(item);
            
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/FavoriteShow/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteFavoriteShow(string id)
        {
            return DeleteAsync(id);
        }
    }
}