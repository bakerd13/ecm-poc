using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DABTechs.eCommerce.Sales.Business.Interfaces;
using DABTechs.eCommerce.Sales.Business.Models.ShoppingBag;

// TODO add logging
namespace DABTechs.eCommerce.Sales.ShoppingBag.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VipShoppingBagController : ControllerBase
    {
        private readonly IVipShoppingBagRepository _vipShoppingBagRepository;

        public VipShoppingBagController(IVipShoppingBagRepository vipShoppingBagRepository)
        {
            _vipShoppingBagRepository = vipShoppingBagRepository;
        }

        [Route("get/{id?}")]
        [HttpGet]
        public async Task<ActionResult<Bag>> GetShoppingBagAsync(string id)
        {
            try
            {
                // Get the API Result.
                var bagResult = await _vipShoppingBagRepository.GetShoppingBagAsync(id);
                
                // Return the bag results.
                return Ok(bagResult);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [Route("create")]
        [HttpPost]
        public async Task<ActionResult<Bag>> CreateShoppingBagAsync(Bag bag)
        {
            try
            {
                // Get the API Result.
                var bagResult = await _vipShoppingBagRepository.CreateShoppingBagAsync(bag);

                // Return the bag results.
                return Ok(bagResult);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}