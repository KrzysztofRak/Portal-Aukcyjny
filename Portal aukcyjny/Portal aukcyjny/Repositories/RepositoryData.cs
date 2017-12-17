using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal_aukcyjny.Repositories
{
    public class UserProfileData
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int SoldItemsNum { get; set; }
    }

    public class AuctionControlData
    {
        public string Title { get; set; }
        public int AuctionId { get; set; }
        public byte[] Image { get; set; }
        public decimal BuyItNowPrice { get; set; }
        public string CurrentPrice { get; set; }
        public string SellerName { get; set; }
        public Guid SellerId { get; set; }
        public string ShipmentName { get; set; }
        public decimal ShipmentPrice { get; set; }
        public DateTime EndDate { get; set; }
        public int OffersNum { get; set; }
        public int Views { get; set; }
    }

    public class CommentControlData
    {
        public string AuthorName { get; set; }
        public Guid AuthorId { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public bool AuthorIsSeller { get; set; }
    }

    public class OfferControlData
    {
        public string BiddrName { get; set; }
        public string BidderId { get; set; }
        public string Price { get; set; }
        public string Date { get; set; }
    }


}