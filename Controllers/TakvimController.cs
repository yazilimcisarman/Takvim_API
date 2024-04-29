using Microsoft.AspNetCore.Mvc;
using Takvim_API.Models;
using Takvim_API.Services.Abstarct;

namespace Takvim_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TakvimController : ControllerBase
    {
        private readonly ITakvimServices _services;

        public TakvimController(ITakvimServices services)
        {
            _services = services;
        }
        //Services çözümlenemedi. Program.csye ekledik.
        //Takvim endpointinid düzenle.
        [HttpGet("getallevent")]
        public async Task<ActionResult> GetAllEvent()
        {
            try
            {
                var events = await _services.GetAllEvent();
                if (events.Count() == 0) 
                {
                    return NotFound(new {success=false,message="Event bulunamadı."});
                }
                return Ok(new {success = true, message=events});
            }
            catch (Exception ex)
            {
                return BadRequest(new {success = false, message = ex.Message});
            }
        }

        //_id değeri göndermeden iletim yapmamız gerekiyor. - TakvimviewModel Oluşturduk
        //Tarih değerlerini düzenleme  yapıcaz // String tarih alıp Parse ile datetime a çevirdik //Saat bilgisini almıyoruz.
        [HttpPost("createevent")]
        public async Task<IActionResult> CreateEvent(TakvimViewModel takvim)
        {
            try
            {
                DateTime startDate = DateTime.Parse(takvim.Start); //Tarih dromatı 12-02-2024
                DateTime endDate = DateTime.Parse(takvim.End);
                Takvim model = new Takvim();
                model.Title = takvim.Title;
                model.Description = takvim.Description;
                model.Start = startDate.Date;
                model.End = endDate.Date;
                model.Location = takvim.Location;

                var result = await _services.CreateEvent(model);
                if (result)
                {
                    return Ok(new {success=true,message="Oluşturuldu", data=model});
                }
                return BadRequest(new {success = false, message="Oluşturulamadı."});
            }
            catch (Exception ex)
            {
                return BadRequest(new {success=false, message = ex.Message});
            }
        }
        [HttpPost("updateevent")]
        public async Task<IActionResult> UpdateEvent(string id, TakvimViewModel takvim)
        {
            try
            {
                DateTime startdate = DateTime.Parse(takvim.Start);
                DateTime enddate = DateTime.Parse(takvim.End); 
                Takvim model = new Takvim();
                model.Title = takvim.Title;
                model.Description = takvim.Description;
                model.Start = startdate.Date;
                model.End = enddate.Date;
                model.Location = takvim.Location;

                var result = await _services.UpdateEvent(id,model);
                if(result == true)
                {
                    return Ok(new {success=true, message="Event Güncellendi.", data=takvim});
                }
                else
                {
                    return BadRequest(new {success = false, message="Event Güncellenmedi."});
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { succes = false, message = ex.Message });
            }
        }

        [HttpGet("geteventbyid")]
        public async Task<ActionResult> GetEventById(string id)
        {
            try
            {
                var takvim = await _services.GetEventById(id);
                if(takvim == null)
                {
                    return NotFound(new {success = false, message="Event Bulunamadı."});
                }
                return Ok(new {success = true, message=takvim});
            }
            catch (Exception ex)
            {
                return BadRequest(new {success = false, message=ex.Message});
            }
        }
        //Tarihe göre Getirme yapılacak

        [HttpGet("geteventbydate")]
        public async Task<ActionResult> GetEventByDate(DateTime start) 
        {
            try
            {
                if(start == null)
                {
                    return BadRequest(new {success = false, message="Tarih boş olamaz"});
                }
                else
                {
                    var events = await _services.GetEventByDate(start);
                    if(events.Count == 0)
                    {
                        return NotFound(new {success = false, message= "Event Bulunamadı."});
                    }
                    else
                    {
                        return Ok(new {success = true,message =  events});
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new {success = false, message=ex.Message});
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteEvent(string id)
        {
            try
            {
                if(id == null)
                {
                    return BadRequest(new { success = false, message = "id null olamaz" });
                }
                else
                {
                    await _services.DeleteEvent(id);
                    return Ok(new {success = true, message="Event silindi."});
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        
    }
}
