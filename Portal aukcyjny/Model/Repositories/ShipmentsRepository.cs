using Model.RepositoriesDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public class ShipmentsRepository
    {
        private PortalAukcyjnyEntities db;

        public ShipmentsRepository(PortalAukcyjnyEntities _db)
        {
            db = _db;
        }

        public ShipmentsRepository()
        {
            db = new PortalAukcyjnyEntities();
        }



        public List<ShipmentsWithFullNames> GetList(CurrencyExchangeRepository currencyRepo, string currencyCode)
        {

            var shipments =
            (from s in db.Shipments select s).ToList();

            var shipmentsWithFullNames = new List<ShipmentsWithFullNames>();

            foreach (var s in shipments)
            {
                shipmentsWithFullNames.Add(new ShipmentsWithFullNames() { Id = s.Id, Name = s.Name + " " + currencyRepo.Exchange(s.Price, currencyCode) });
            }

            return shipmentsWithFullNames;
        }

        public string GetShipmentFullName(CurrencyExchangeRepository currencyRepo, int shipmentId)
        {
            var shipment =
            (from s in db.Shipments
             where s.Id == shipmentId
             select s).First();

            var shipmentName = shipment.Name + " " + currencyRepo.Exchange(shipment.Price);

            return shipmentName;
        }
    }
}
