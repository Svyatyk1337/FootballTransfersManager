using FootballTransfers.Core.Entities;
using FootballTransfers.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public static class DbSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        if (context.Clubs.Any() || context.Agents.Any() || context.Players.Any() || context.Transfers.Any()) return;

        DateTime ToUtc(DateTime dt) => dt.Kind == DateTimeKind.Utc ? dt : DateTime.SpecifyKind(dt, DateTimeKind.Utc);

        var now = DateTime.UtcNow;

        var clubs = new List<Club>
        {
            new() { Name = "Real Madrid", Country = "Spain", League = "La Liga", City = "Madrid", Founded = ToUtc(new DateTime(1902,3,6)), Stadium = "Santiago Bernabéu", CreatedAt = now, UpdatedAt = now },
            new() { Name = "PSG", Country = "France", League = "Ligue 1", City = "Paris", Founded = ToUtc(new DateTime(1970,8,12)), Stadium = "Parc des Princes", CreatedAt = now, UpdatedAt = now },
            new() { Name = "Al-Nassr", Country = "Saudi Arabia", League = "Saudi Pro League", City = "Riyadh", Founded = ToUtc(new DateTime(1955,10,1)), Stadium = "Al-Awwal Park", CreatedAt = now, UpdatedAt = now },
            new() { Name = "Manchester City", Country = "England", League = "Premier League", City = "Manchester", Founded = ToUtc(new DateTime(1880,4,16)), Stadium = "Etihad", CreatedAt = now, UpdatedAt = now },
            new() { Name = "Sevilla", Country = "Spain", League = "La Liga", City = "Sevilla", Founded = ToUtc(new DateTime(1890,1,25)), Stadium = "Ramón Sánchez Pizjuán", CreatedAt = now, UpdatedAt = now },
            new() { Name = "Monaco", Country = "France", League = "Ligue 1", City = "Monaco", Founded = ToUtc(new DateTime(1924,8,1)), Stadium = "Stade Louis II", CreatedAt = now, UpdatedAt = now },
            new() { Name = "Manchester United", Country = "England", League = "Premier League", City = "Manchester", Founded = ToUtc(new DateTime(1878,3,5)), Stadium = "Old Trafford", CreatedAt = now, UpdatedAt = now }
        };
        context.Clubs.AddRange(clubs);
        context.SaveChanges();

        var agents = new List<Agent>
        {
            new() { FirstName = "Jean", LastName = "Dupont", Email = "jean@example.com", Phone = "+33123456789", CreatedAt = now, UpdatedAt = now },
            new() { FirstName = "Maria", LastName = "Gomez", Email = "maria@example.com", Phone = "+34987654321", CreatedAt = now, UpdatedAt = now },
            new() { FirstName = "Ali", LastName = "Hassan", Email = "ali@example.com", Phone = "+966500000001", CreatedAt = now, UpdatedAt = now },
            new() { FirstName = "John", LastName = "Smith", Email = "john.smith@example.com", Phone = "+447911123456", CreatedAt = now, UpdatedAt = now },
            new() { FirstName = "Pedro", LastName = "Lopez", Email = "pedro@example.com", Phone = "+34123456789", CreatedAt = now, UpdatedAt = now }
        };
        context.Agents.AddRange(agents);
        context.SaveChanges();

        var players = new List<Player>
        {
            new() { FirstName = "Kylian", LastName = "Mbappé", DateOfBirth = ToUtc(new DateTime(1998,12,20)), Nationality = "France", Position = Position.Forward, Height = 178, Weight = 73, MarketValue = 180_000_000, CurrentClub = clubs.First(c => c.Name == "PSG"), Agent = agents[0], CreatedAt = now, UpdatedAt = now },
            new() { FirstName = "Cristiano", LastName = "Ronaldo", DateOfBirth = ToUtc(new DateTime(1985,2,5)), Nationality = "Portugal", Position = Position.Forward, Height = 187, Weight = 83, MarketValue = 20_000_000, CurrentClub = clubs.First(c => c.Name == "Al-Nassr"), Agent = agents[2], CreatedAt = now, UpdatedAt = now },
            new() { FirstName = "Jude", LastName = "Bellingham", DateOfBirth = ToUtc(new DateTime(2003,6,29)), Nationality = "England", Position = Position.Midfielder, Height = 186, Weight = 75, MarketValue = 120_000_000, CurrentClub = clubs.First(c => c.Name == "Real Madrid"), Agent = agents[1], CreatedAt = now, UpdatedAt = now },
            new() { FirstName = "Sergio", LastName = "Ramos", DateOfBirth = ToUtc(new DateTime(1986,3,30)), Nationality = "Spain", Position = Position.Defender, Height = 184, Weight = 82, MarketValue = 4_000_000, CurrentClub = clubs.First(c => c.Name == "Sevilla"), Agent = agents[4], CreatedAt = now, UpdatedAt = now },
            new() { FirstName = "Erling", LastName = "Haaland", DateOfBirth = ToUtc(new DateTime(2000,7,21)), Nationality = "Norway", Position = Position.Forward, Height = 194, Weight = 88, MarketValue = 170_000_000, CurrentClub = clubs.First(c => c.Name == "Manchester City"), Agent = agents[3], CreatedAt = now, UpdatedAt = now }
        };
        context.Players.AddRange(players);
        context.SaveChanges();

        var transfers = new List<Transfer>
        {
            new() { Player = players[0], FromClub = clubs.First(c => c.Name == "Monaco"), ToClub = clubs.First(c => c.Name == "PSG"), TransferFee = 180_000_000, TransferDate = ToUtc(new DateTime(2017,8,31)), TransferType = TransferType.Permanent, ContractLength = "5 років", IsConfirmed = true, CreatedAt = now, UpdatedAt = now },
            new() { Player = players[1], FromClub = clubs.First(c => c.Name == "Manchester United"), ToClub = clubs.First(c => c.Name == "Al-Nassr"), TransferFee = 0, TransferDate = ToUtc(new DateTime(2022,12,30)), TransferType = TransferType.Free, ContractLength = "2.5 роки", IsConfirmed = true, CreatedAt = now, UpdatedAt = now },
            new() { Player = players[2], FromClub = clubs.First(c => c.Name == "Manchester United"), ToClub = clubs.First(c => c.Name == "Real Madrid"), TransferFee = 103_000_000, TransferDate = ToUtc(new DateTime(2023,6,14)), TransferType = TransferType.Permanent, ContractLength = "6 років", IsConfirmed = true, CreatedAt = now, UpdatedAt = now },
            new() { Player = players[3], FromClub = clubs.First(c => c.Name == "PSG"), ToClub = clubs.First(c => c.Name == "Sevilla"), TransferFee = 0, TransferDate = ToUtc(new DateTime(2023,9,4)), TransferType = TransferType.Free, ContractLength = "1 рік", IsConfirmed = true, CreatedAt = now, UpdatedAt = now },
            new() { Player = players[4], FromClub = clubs.First(c => c.Name == "Monaco"), ToClub = clubs.First(c => c.Name == "Manchester City"), TransferFee = 60_000_000, TransferDate = ToUtc(new DateTime(2022,6,13)), TransferType = TransferType.Permanent, ContractLength = "5 років", IsConfirmed = true, CreatedAt = now, UpdatedAt = now }
        };
        context.Transfers.AddRange(transfers);
        context.SaveChanges();
    }
}
