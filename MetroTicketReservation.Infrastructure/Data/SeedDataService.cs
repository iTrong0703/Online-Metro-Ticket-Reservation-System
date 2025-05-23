﻿using MetroTicketReservation.Application.Common.Interfaces;
using MetroTicketReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MetroTicketReservation.Infrastructure.Data
{
    public class SeedDataService : ISeedDataService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public SeedDataService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task SeedAsync()
        {
            string basePath = Path.GetFullPath(_configuration["SeedDataPath"]!);
            if (!await _context.Stations.AnyAsync() || !await _context.Lines.AnyAsync())
            {
                
                string stationFilePath = Path.Combine(basePath, "Stations.json");
                string lineFilePath = Path.Combine(basePath, "Lines.json");

                var stations = ReadJsonFile<List<Station>>(stationFilePath);
                var lines = ReadJsonFile<List<Line>>(lineFilePath);

                if (stations?.Count > 0)
                {
                    await _context.Stations.AddRangeAsync(stations);
                }
                if (lines?.Count > 0)
                {
                    await _context.Lines.AddRangeAsync(lines);
                }
                await _context.SaveChangesAsync();
                // Seed StationLine
                var dbStations = await _context.Stations.ToListAsync();
                var dbLines = await _context.Lines.ToListAsync();

                if (dbStations.Count > 1 && dbLines.Count > 0)
                {
                    var stationLines = new List<StationLine>();
                    foreach (var line in dbLines)
                    {
                        for (int i = 0; i < dbStations.Count; i++)
                        {
                            stationLines.Add(new StationLine
                            {
                                StationID = dbStations[i].StationID,
                                LineID = line.LineID,
                                StationOrder = i + 1
                            });
                        }
                    }
                    await _context.StationLines.AddRangeAsync(stationLines);
                    await _context.SaveChangesAsync();
                }
            }
            if (!await _context.TicketTypes.AnyAsync())
            {
                string ticketTypeFilePath = Path.Combine(basePath, "TicketTypes.json");
                var ticketTypes = ReadJsonFile<List<TicketType>>(ticketTypeFilePath);
                if (ticketTypes?.Count > 0)
                {
                    await _context.TicketTypes.AddRangeAsync(ticketTypes);
                }
                await _context.SaveChangesAsync();
            }
            if (!await _context.StationFares.AnyAsync())
            {
                string stationFareFilePath = Path.Combine(basePath, "StationFares.json");
                var stationFares = ReadJsonFile<List<StationFare>>(stationFareFilePath);
                var checkTimeBased = await _context.TicketTypes.AnyAsync(t => t.IsTimeBased == false);
                if (checkTimeBased && stationFares?.Count > 0)
                {
                    await _context.StationFares.AddRangeAsync(stationFares);
                }
                await _context.SaveChangesAsync();
            }    
            return;
        }

        private static T? ReadJsonFile<T>(string path) where T : class // giới hạn T chỉ đc là class, k cho int, string,...
        {
            if (!File.Exists(path)) return null;
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}

    
