//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lab2_Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int Id { get; set; }
        public int Client_id { get; set; }
        public int Service_id { get; set; }
        public int Price { get; set; }
        public string Note { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Service Service { get; set; }
    }
}