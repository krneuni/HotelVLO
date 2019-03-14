using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VLO.Models;

namespace VLO.Controllers
{
    public class OrdenesController : Controller
    {
        private Context db = new Context();

        // GET: Ordenes
        public ActionResult Index()
        {
            return View(db.Mesa.ToList());
        }

        // GET: Ordenes
        public ActionResult Menu(int? id)
        {
            ViewBag.mesa = id;
            OrdenesViewModel ovm = new OrdenesViewModel();
            ovm.Menus = db.Menus.ToList();
            ovm.TiposMenu = db.TipoMenus.ToList();
            return View(ovm);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrden(AddOrdenViewModel aovm)
        {
            Empleado e = db.Empleado.Where(x => x.IdEmpleado == 1).FirstOrDefault();

            Pedido p = new Pedido();
            p.Cantidad = aovm.numpersonas;
            p.Cliente = aovm.cliente;
            p.Empleado = e;
            p.Estado = 1;
            p.IdMesa = aovm.mesa;

            db.Pedido.Add(p);
            await db.SaveChangesAsync();

            var lastPedido = db.Pedido.OrderByDescending(x=>x.IdPedido).First();
            for(var i= 0;i < aovm.id.Count;i++)
            {
                DetallePedido dp = new DetallePedido();
                dp.IdMenu = aovm.id[i];
                dp.cantidad = aovm.cantidad[i];
                dp.IdPedido = lastPedido.IdPedido;

                db.DetallePedido.Add(dp);
                await db.SaveChangesAsync();

                //Encontrar el menu
                var menu = db.Menus.Find(dp.IdMenu);
                //Buscar los menus en la receta
                var recmenu = (from u in db.Receta where u.IdMenu == menu.IdMenu select u).ToList();
                //Recorrer
                foreach (var io in recmenu)
                {
                    //Encontrar los productos que se utilizan
                    Productos de = db.Productos.Find(io.IdProducto);
                    
                    //Resta de la cantidad que se pide menos la cantidad utilizada

                    var Descuento = io.CantidadUtilizada * dp.cantidad;
                    de.Cantidad =de.Cantidad - Descuento;
                    db.Entry(de).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            //Encontrar las mesas
            Mesa d = db.Mesa.Find(aovm.mesa);
            //Cambia el estado de la mesa
            d.Estado = false;
            db.Entry(d).State = EntityState.Modified;
            db.SaveChanges();

            return Redirect("/Ordenes");
        }
    }
}