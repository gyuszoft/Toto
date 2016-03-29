using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCToto.Models.Toto {
    public class TotoContext : DbContext {
        //public DbSet<TotoTippSor> Tippsor { get; set; }

        protected override void OnModelCreating( DbModelBuilder modelBuilder ) {
            //Felüldefiniáljuk az alap táblakezelése, enélkül a productba kerül
            //modelBuilder.Entity<DiscountedProduct>().ToTable( "DiscountedProducts" );

            // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ProductContext>());
            Database.SetInitializer( new TotoInitializer() );
        }
    }

    public class TotoInitializer : DropCreateDatabaseAlways<TotoContext> {
        protected override void Seed( TotoContext context ) {
            //var t = new TotoTippSor();
            //var e = new TotoEgyOszlop();
            //for(int i = 0; i < 14; i++) {
            //    e.EgyOszlop[i] = new TotoTipp();
            //    e.EgyOszlop[i].Tipp1.Tipp = BASETIPP._1 ;
            //    e.EgyOszlop[i].Tipp2.Tipp = BASETIPP._2 ;
            //    e.EgyOszlop[i].Tipp3.Tipp = BASETIPP._2;
            //}
            //for(int k = 0; k < 30; k++) {
            //    t.TippSor.Add( e );
            //}
        }
    }
}