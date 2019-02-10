using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeus;

namespace Dohko
{
    class InventoryCarParts : InventoryBase
    {
        #region Constructors
        public InventoryCarParts(string filePath) : base(filePath)
        {

        }
        #endregion

        #region Override Methods

        //Methods that need to be overriden:
        //GetProduct()
        //GetProductFromDescription()
        //UpdateProductToTable()
        //AddNewProductToTable()
        //Search()

        //Columns that are required:
        //"Codigo"
        //"Id"
        //"NumeroProducto"
        //"Descripcion"
        //"CantidadDisponibleTotal"
        //"Precio"
        //"CantidadVendidoHistorial"
        //"VendidoHistorial"
        //"CantidadInternoHistorial"
        //"CantidadLocal"
        //"UltimaTransaccionFecha"          

        public override IProduct GetProduct(string code)
        {
            try
            {
                for (int index = 0; index < base.DictOfData.Rows.Count; index++)
                {
                    var row = base.DictOfData.Rows[index];
                    if (row["Codigo"].ToString() == code)
                    {
                        return new ProductBase()
                        {
                            Id = Int32.Parse(row["NumeroProducto"].ToString()),
                            Code = row["Codigo"].ToString(),
                            AlternativeCode = row["CodigoAlterno"].ToString(),
                            ProviderProductId = row["ProveedorProductoId"].ToString(),
                            Description = row["Descripcion"].ToString(),
                            Provider = row["Proveedor"].ToString(),
                            Category = row["Categoria"].ToString(),
                            LastPurchaseDate = Convert.ToDateTime(row["UltimoPedidoFecha"].ToString()),
                            Cost = Decimal.Parse(row["Costo"].ToString()),
                            CostCurrency = row["CostoMoneda"].ToString().ToUpper() == "USD" ? CurrencyTypeEnum.USD : CurrencyTypeEnum.MXN,
                            Price = decimal.Parse(row["Precio"].ToString()),
                            PriceCurrency = row["PrecioMoneda"].ToString().ToUpper() == "USD" ? CurrencyTypeEnum.USD : CurrencyTypeEnum.MXN,
                            InternalQuantity = Int32.Parse(row["CantidadInternoHistorial"].ToString()),
                            QuantitySold = Int32.Parse(row["CantidadVendidoHistorial"].ToString()),
                            AmountSold = decimal.Parse(row["VendidoHistorial"].ToString()),
                            LocalQuantityAvailable = Int32.Parse(row["CantidadLocal"].ToString()),
                            TotalQuantityAvailable = Int32.Parse(row["CantidadDisponibleTotal"].ToString()),
                            MinimumStockQuantity = Int32.Parse(row["CantidadMinima"].ToString()),
                            LastSaleDate = Convert.ToDateTime(row["UltimaTransaccionFecha"].ToString()),
                            ImageName = row["Imagen"].ToString()
                        };
                    }
                }
            }
            catch (Exception e)
            {
                
            }

            return new ProductBase() { Description = "", Category = "", Cost = 0M };
        }

        public override IProduct GetProductFromDescription(string description)
        {
            try
            {
                for (int index = 0; index < base.DictOfData.Rows.Count; index++)
                {
                    var row = base.DictOfData.Rows[index];
                    if (row["Descripcion"].ToString() == description)
                    {
                        return new ProductBase()
                        {
                            Id = Int32.Parse(row["NumeroProducto"].ToString()),
                            Code = row["Codigo"].ToString(),
                            AlternativeCode = row["CodigoAlterno"].ToString(),
                            ProviderProductId = row["ProveedorProductoId"].ToString(),
                            Description = row["Descripcion"].ToString(),
                            Provider = row["Proveedor"].ToString(),
                            Category = row["Categoria"].ToString(),
                            LastPurchaseDate = Convert.ToDateTime(row["UltimoPedidoFecha"].ToString()),
                            Cost = Decimal.Parse(row["Costo"].ToString()),
                            CostCurrency = row["CostoMoneda"].ToString().ToUpper() == "USD" ? CurrencyTypeEnum.USD : CurrencyTypeEnum.MXN,
                            Price = decimal.Parse(row["Precio"].ToString()),
                            PriceCurrency = row["PrecioMoneda"].ToString().ToUpper() == "USD" ? CurrencyTypeEnum.USD : CurrencyTypeEnum.MXN,
                            InternalQuantity = Int32.Parse(row["CantidadInternoHistorial"].ToString()),
                            QuantitySold = Int32.Parse(row["CantidadVendidoHistorial"].ToString()),
                            AmountSold = decimal.Parse(row["VendidoHistorial"].ToString()),
                            LocalQuantityAvailable = Int32.Parse(row["CantidadLocal"].ToString()),
                            TotalQuantityAvailable = Int32.Parse(row["CantidadDisponibleTotal"].ToString()),
                            MinimumStockQuantity = Int32.Parse(row["CantidadMinima"].ToString()),
                            LastSaleDate = Convert.ToDateTime(row["UltimaTransaccionFecha"].ToString()),
                            ImageName = row["Imagen"].ToString()
                        };
                    }
                }
            }
            catch (Exception e)
            {

            }

            return new ProductBase() { Description = "", Category = "", Cost = 0M };
        }

        public override bool UpdateProductToTable(IProduct product)
        {
            for (int index = 0; index < base.DictOfData.Rows.Count; index++)
            {
                var row = base.DictOfData.Rows[index];
                if (row["Codigo"].ToString() == product.Code)
                {
                    row["NumeroProducto"] = product.Id.ToString();
                    row["CodigoAlterno"] = product.AlternativeCode;
                    row["ProveedorProductoId"] = product.ProviderProductId;
                    row["Descripcion"] = product.Description;
                    row["Proveedor"] = product.Provider;
                    row["Categoria"] = product.Category;
                    row["Costo"] = product.Cost.ToString(CultureInfo.InvariantCulture);
                    row["CostoMoneda"] = product.CostCurrency;
                    row["Precio"] = product.Price.ToString();
                    row["PrecioMoneda"] = product.PriceCurrency.ToString();
                    row["CantidadInternoHistorial"] = product.InternalQuantity.ToString();
                    row["CantidadVendidoHistorial"] = product.QuantitySold.ToString();
                    row["CantidadLocal"] = product.LocalQuantityAvailable.ToString();
                    row["VendidoHistorial"] = product.AmountSold.ToString();
                    row["CantidadDisponibleTotal"] = product.TotalQuantityAvailable.ToString();
                    row["CantidadMinima"] = product.MinimumStockQuantity.ToString();
                    row["UltimoPedidoFecha"] = product.LastPurchaseDate.ToString();
                    row["UltimaTransaccionFecha"] = product.LastSaleDate.ToString();
                    row["Imagen"] = product.ImageName;
                }
            }

            return true;
        }

        public override bool AddNewProductToTable(IProduct product)
        {
            base.DictOfData.Rows.Add();
            var row = base.DictOfData.Rows[base.DictOfData.Rows.Count - 1];
            row["NumeroProducto"] = product.Id.ToString();
            row["Codigo"] = product.Code;
            row["CodigoAlterno"] = product.AlternativeCode;
            row["ProveedorProductoId"] = product.ProviderProductId;
            row["Descripcion"] = product.Description;
            row["Proveedor"] = product.Provider;
            row["Categoria"] = product.Category;
            row["Costo"] = product.Cost.ToString(CultureInfo.InvariantCulture);
            row["CostoMoneda"] = product.CostCurrency;
            row["Precio"] = product.Price.ToString();
            row["PrecioMoneda"] = product.PriceCurrency;
            row["CantidadInternoHistorial"] = product.InternalQuantity.ToString();
            row["CantidadVendidoHistorial"] = product.QuantitySold.ToString();
            row["CantidadLocal"] = product.LocalQuantityAvailable.ToString();
            row["CantidadDisponibleTotal"] = product.TotalQuantityAvailable.ToString();
            row["VendidoHistorial"] = product.AmountSold.ToString();
            row["CantidadMinima"] = product.MinimumStockQuantity.ToString();
            row["UltimoPedidoFecha"] = product.LastPurchaseDate.ToString();
            row["UltimaTransaccionFecha"] = product.LastSaleDate.ToString();
            row["Imagen"] = product.ImageName;

            return true;
        }

        public override List<IProduct> Search(string input)
        {
            var products = new List<IProduct>();

            //Return empty list if invalid inputs are entered for the search
            if (string.IsNullOrWhiteSpace(input) || input == "x")
                return products;

            if (input == "*")
            {
                var allProducts = base.DictOfData.AsEnumerable();
                foreach (var row in allProducts)
                {

                    var product = new ProductBase()
                    {
                        Id = Int32.Parse(row["NumeroProducto"].ToString()),
                        Code = row["Codigo"].ToString(),
                        AlternativeCode = row["CodigoAlterno"].ToString(),
                        ProviderProductId = row["ProveedorProductoId"].ToString(),
                        Description = row["Descripcion"].ToString(),
                        Provider = row["Proveedor"].ToString(),
                        Category = row["Categoria"].ToString(),
                        LastPurchaseDate = Convert.ToDateTime(row["UltimoPedidoFecha"].ToString()),
                        Cost = Decimal.Parse(row["Costo"].ToString()),
                        Price = decimal.Parse(row["Precio"].ToString()),
                        InternalQuantity = Int32.Parse(row["CantidadInternoHistorial"].ToString()),
                        QuantitySold = Int32.Parse(row["CantidadVendidoHistorial"].ToString()),
                        AmountSold = decimal.Parse(row["VendidoHistorial"].ToString()),
                        LocalQuantityAvailable = Int32.Parse(row["CantidadLocal"].ToString()),
                        TotalQuantityAvailable = Int32.Parse(row["CantidadDisponibleTotal"].ToString()),
                        MinimumStockQuantity = Int32.Parse(row["CantidadMinima"].ToString()),
                        LastSaleDate = Convert.ToDateTime(row["UltimaTransaccionFecha"].ToString()),
                        ImageName = row["Imagen"].ToString()
                    };

                    product.CostCurrency = row["CostoMoneda"].ToString().ToUpper() == "USD" ? CurrencyTypeEnum.USD : CurrencyTypeEnum.MXN;
                    product.PriceCurrency = row["PrecioMoneda"].ToString().ToUpper() == "USD" ? CurrencyTypeEnum.USD : CurrencyTypeEnum.MXN;

                    products.Add(product);
                }

                return products;
            }

            var descriptionFilter = base.DictOfData.AsEnumerable().Where(r => r.Field<string>("Descripcion").ToLower().Contains(input));
            var codeFilter = base.DictOfData.AsEnumerable().Where(r => r.Field<string>("Codigo").ToLower().Contains(input));

            foreach (var row in codeFilter)
            {
                var product = new ProductBase()
                {
                    Id = Int32.Parse(row["NumeroProducto"].ToString()),
                    Code = row["Codigo"].ToString(),
                    AlternativeCode = row["CodigoAlterno"].ToString(),
                    ProviderProductId = row["ProveedorProductoId"].ToString(),
                    Description = row["Descripcion"].ToString(),
                    Provider = row["Proveedor"].ToString(),
                    Category = row["Categoria"].ToString(),
                    LastPurchaseDate = Convert.ToDateTime(row["UltimoPedidoFecha"].ToString()),
                    Cost = Decimal.Parse(row["Costo"].ToString()),
                    Price = decimal.Parse(row["Precio"].ToString()),
                    InternalQuantity = Int32.Parse(row["CantidadInternoHistorial"].ToString()),
                    QuantitySold = Int32.Parse(row["CantidadVendidoHistorial"].ToString()),
                    AmountSold = decimal.Parse(row["VendidoHistorial"].ToString()),
                    LocalQuantityAvailable = Int32.Parse(row["CantidadLocal"].ToString()),
                    TotalQuantityAvailable = Int32.Parse(row["CantidadDisponibleTotal"].ToString()),
                    MinimumStockQuantity = Int32.Parse(row["CantidadMinima"].ToString()),
                    LastSaleDate = Convert.ToDateTime(row["UltimaTransaccionFecha"].ToString()),
                    ImageName = row["Imagen"].ToString()
                };

                product.CostCurrency = row["CostoMoneda"].ToString().ToUpper() == "USD" ? CurrencyTypeEnum.USD : CurrencyTypeEnum.MXN;
                product.PriceCurrency = row["PrecioMoneda"].ToString().ToUpper() == "USD" ? CurrencyTypeEnum.USD : CurrencyTypeEnum.MXN;

                products.Add(product);
            }

            foreach (var row in descriptionFilter)
            {
                var product = new ProductBase()
                {
                    Id = Int32.Parse(row["NumeroProducto"].ToString()),
                    Code = row["Codigo"].ToString(),
                    AlternativeCode = row["CodigoAlterno"].ToString(),
                    ProviderProductId = row["ProveedorProductoId"].ToString(),
                    Description = row["Descripcion"].ToString(),
                    Provider = row["Proveedor"].ToString(),
                    Category = row["Categoria"].ToString(),
                    LastPurchaseDate = Convert.ToDateTime(row["UltimoPedidoFecha"].ToString()),
                    Cost = Decimal.Parse(row["Costo"].ToString()),
                    Price = decimal.Parse(row["Precio"].ToString()),
                    InternalQuantity = Int32.Parse(row["CantidadInternoHistorial"].ToString()),
                    QuantitySold = Int32.Parse(row["CantidadVendidoHistorial"].ToString()),
                    AmountSold = decimal.Parse(row["VendidoHistorial"].ToString()),
                    LocalQuantityAvailable = Int32.Parse(row["CantidadLocal"].ToString()),
                    TotalQuantityAvailable = Int32.Parse(row["CantidadDisponibleTotal"].ToString()),
                    MinimumStockQuantity = Int32.Parse(row["CantidadMinima"].ToString()),
                    LastSaleDate = Convert.ToDateTime(row["UltimaTransaccionFecha"].ToString()),
                    ImageName = row["Imagen"].ToString()
                };

                product.CostCurrency = row["CostoMoneda"].ToString().ToUpper() == "USD" ? CurrencyTypeEnum.USD : CurrencyTypeEnum.MXN;
                product.PriceCurrency = row["PrecioMoneda"].ToString().ToUpper() == "USD" ? CurrencyTypeEnum.USD : CurrencyTypeEnum.MXN;

                //Add if it does not exist already
                if (!products.Exists(x => x.Code == product.Code))
                    products.Add(product);
            }

            return products;
        }

        #endregion
    }
}
