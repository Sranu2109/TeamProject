using API.Dtos;
using AutoMapper;
using core.Entities;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        private readonly StoreContext _context;

        public BasketController(IBasketRepository basketRepository, IMapper mapper, StoreContext context)
        {
            _context = context;
            _mapper = mapper;
            _basketRepository = basketRepository;
        }

        [HttpGet("invoice")]      //endpoint
        public async Task<ActionResult<List<sellerinvoice>>> sellerinvoice(string name)
        {
          var list = await _context.sellerinvoices.ToListAsync();
          var res = new List<sellerinvoice>();
          foreach(var x in list)
          {
              if(x.sellername == name)
              {
                  res.Add(x);
              }
          }   
          return res;         
        }

        [HttpPost("Ranu")]
        public async Task<ActionResult<string>> ranu()
        {
            //Int32.Parse()
            var  id = Int32.Parse(Request.Form["hhh"]);
        
             var y = await _context.productlists.ToListAsync();
                       System.Console.WriteLine("*****");
                        

               foreach(var z in y)
               {
                   if(z.productid==id)
                   {
                        var item = new sellerinvoice();
                        item.productid=z.productid;
                        item.quantity=Int32.Parse(Request.Form["quantity"]);
                        item.totalprice=Int32.Parse(Request.Form["totalprice"]);
                        item.imageurl=Request.Form["imageurl"];
                        item.productname=Request.Form["proname"];
                        item.sellername=z.sellername;
                        item.OrderDate=DateTimeOffset.Now;
                       await _context.sellerinvoices.AddAsync(item);
                      await  _context.SaveChangesAsync();

                     

                       break;
                   }
               }
               return Ok("successs");
            
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);

            return Ok(basket ?? new CustomerBasket(id));     //if no basket then create new basket with no items
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var customerBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);

            var updatedBasket = await _basketRepository.UpdateBasketAsync(customerBasket);

            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasketAsync(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
    }
}