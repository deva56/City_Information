namespace CityInformation.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelsContext : DbContext
    {
        public ModelsContext()
            : base("name=MainConnection")
        {
        }

        public virtual DbSet<administrator> administrator { get; set; }
        public virtual DbSet<aspnetrole> aspnetroles { get; set; }
        public virtual DbSet<aspnetuserclaim> aspnetuserclaims { get; set; }
        public virtual DbSet<aspnetuserlogin> aspnetuserlogins { get; set; }
        public virtual DbSet<aspnetuser> aspnetusers { get; set; }
        public virtual DbSet<djelatnost> djelatnost { get; set; }
        public virtual DbSet<grad> grad { get; set; }
        public virtual DbSet<korisnik> korisnik { get; set; }
        public virtual DbSet<poduzeće> poduzeće { get; set; }
        public virtual DbSet<recenzija> recenzija { get; set; }
        public virtual DbSet<slika> slika { get; set; }
        public virtual DbSet<ulica> ulica { get; set; }
        public virtual DbSet<županija> županija { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<administrator>()
                .Property(e => e.idAdministratora)
                .IsUnicode(false);

            modelBuilder.Entity<administrator>()
                .Property(e => e.imeAdministratora)
                .IsUnicode(false);

            modelBuilder.Entity<aspnetrole>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<aspnetrole>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<aspnetrole>()
                .HasMany(e => e.aspnetusers)
                .WithMany(e => e.aspnetroles)
                .Map(m => m.ToTable("aspnetuserroles", "cityinformation").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<aspnetuserclaim>()
                .Property(e => e.UserId)
                .IsUnicode(false);

            modelBuilder.Entity<aspnetuserclaim>()
                .Property(e => e.ClaimType)
                .IsUnicode(false);

            modelBuilder.Entity<aspnetuserclaim>()
                .Property(e => e.ClaimValue)
                .IsUnicode(false);

            modelBuilder.Entity<aspnetuserlogin>()
                .Property(e => e.LoginProvider)
                .IsUnicode(false);

            modelBuilder.Entity<aspnetuserlogin>()
                .Property(e => e.ProviderKey)
                .IsUnicode(false);

            modelBuilder.Entity<aspnetuserlogin>()
                .Property(e => e.UserId)
                .IsUnicode(false);

            modelBuilder.Entity<aspnetuser>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<aspnetuser>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<aspnetuser>()
                .Property(e => e.PasswordHash)
                .IsUnicode(false);

            modelBuilder.Entity<aspnetuser>()
                .Property(e => e.SecurityStamp)
                .IsUnicode(false);

            modelBuilder.Entity<aspnetuser>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<aspnetuser>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<aspnetuser>()
                .HasMany(e => e.aspnetuserclaims)
                .WithRequired(e => e.aspnetuser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<aspnetuser>()
                .HasMany(e => e.aspnetuserlogins)
                .WithRequired(e => e.aspnetuser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<djelatnost>()
                .Property(e => e.imeDjelatnost)
                .IsUnicode(false);

            modelBuilder.Entity<djelatnost>()
                .HasMany(e => e.poduzeće)
                .WithOptional(e => e.djelatnost)
                .HasForeignKey(e => e.tipPoslovnogObjekta)
                .WillCascadeOnDelete();

            modelBuilder.Entity<grad>()
                .Property(e => e.imeGrad)
                .IsUnicode(false);

            modelBuilder.Entity<grad>()
                .HasMany(e => e.poduzeće)
                .WithOptional(e => e.grad)
                .HasForeignKey(e => e.gradPoduzeće)
                .WillCascadeOnDelete();

            modelBuilder.Entity<korisnik>()
                .Property(e => e.idKorisnik)
                .IsUnicode(false);

            modelBuilder.Entity<korisnik>()
                .Property(e => e.registracijskiEmailKorisnik)
                .IsUnicode(false);

            modelBuilder.Entity<korisnik>()
                .Property(e => e.putanjaDoProfilneSlike)
                .IsUnicode(false);

            modelBuilder.Entity<korisnik>()
                .Property(e => e.korisničkoImeKorisnik)
                .IsUnicode(false);

            modelBuilder.Entity<korisnik>()
                .HasMany(e => e.recenzijas)
                .WithRequired(e => e.korisnik)
                .HasForeignKey(e => e.vlasnikRecenzijaID);

            modelBuilder.Entity<poduzeće>()
                .Property(e => e.idPoduzeće)
                .IsUnicode(false);

            modelBuilder.Entity<poduzeće>()
                .Property(e => e.imePoduzeće)
                .IsUnicode(false);

            modelBuilder.Entity<poduzeće>()
                .Property(e => e.opisPoduzeće)
                .IsUnicode(false);

            modelBuilder.Entity<poduzeće>()
                .Property(e => e.kontaktTelefon)
                .IsUnicode(false);

            modelBuilder.Entity<poduzeće>()
                .Property(e => e.kontaktEmail)
                .IsUnicode(false);

            modelBuilder.Entity<poduzeće>()
                .Property(e => e.korisničkoImePoduzeće)
                .IsUnicode(false);

            modelBuilder.Entity<poduzeće>()
                .HasMany(e => e.slikas)
                .WithRequired(e => e.poduzeće)
                .HasForeignKey(e => e.idPoduzeća);

            modelBuilder.Entity<poduzeće>()
                .HasMany(e => e.recenzijas)
                .WithRequired(e => e.poduzeće)
                .HasForeignKey(e => e.poduzećeRecenzijaID);

            modelBuilder.Entity<recenzija>()
                .Property(e => e.tekstRecenzija)
                .IsUnicode(false);

            modelBuilder.Entity<recenzija>()
                .Property(e => e.vlasnikRecenzijaID)
                .IsUnicode(false);

            modelBuilder.Entity<recenzija>()
                .Property(e => e.datumRecenzija)
                .IsUnicode(false);

            modelBuilder.Entity<recenzija>()
                .Property(e => e.poduzećeRecenzijaID)
                .IsUnicode(false);

            modelBuilder.Entity<slika>()
                .Property(e => e.idSlika)
                .IsUnicode(false);

            modelBuilder.Entity<slika>()
                .Property(e => e.putanjaSlike)
                .IsUnicode(false);

            modelBuilder.Entity<slika>()
                .Property(e => e.idPoduzeća)
                .IsUnicode(false);

            modelBuilder.Entity<ulica>()
                .Property(e => e.imeUlica)
                .IsUnicode(false);

            modelBuilder.Entity<ulica>()
                .HasMany(e => e.poduzeće)
                .WithOptional(e => e.ulica)
                .HasForeignKey(e => e.ulicaPoduzeće)
                .WillCascadeOnDelete();

            modelBuilder.Entity<županija>()
                .Property(e => e.imeŽupanija)
                .IsUnicode(false);

            modelBuilder.Entity<županija>()
                .HasMany(e => e.poduzeće)
                .WithOptional(e => e.županija)
                .HasForeignKey(e => e.županijaPoduzeće)
                .WillCascadeOnDelete();
        }
    }
}
