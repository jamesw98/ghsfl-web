using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using GhsflUtils.Enums;
using GhsflUtils.Exceptions;
using GhsflUtils.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace GhsflUtils.Services;

public class CsvService
{
    public async Task<RosterSubmission> ReadRosterFile(IBrowserFile file, Genders gender, long round) 
    {
        using var reader = new StreamReader(file.OpenReadStream());
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLower()
        });

        IAsyncEnumerable<Name> records;
        records = csv.GetRecordsAsync<Name>();

        List<Name> names = new();
        await foreach(var n in records)
            names.Add(n);

        return new RosterSubmission
        {
            Round = round, 
            Gender = gender, 
            FencerNames = names
        };
    }
    
    public async Task<List<Result>> ReadPointsCsvFtl(IBrowserFile file)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLower()
        });

        IAsyncEnumerable<PointsRow> records;
        try
        {
            records = csv.GetRecordsAsync<PointsRow>();
        }
        catch (Exception e)
        {
            throw new ImportException("Csv import failed", e);
        }

        List<Result> results = new();
        await foreach (var r in records)
        {
            var split = r.Name.ToLower().Split(" ");
            var name = new Name { First = split[1], Last = split[0] };

            if (!int.TryParse(r.Place, out var place))
            {
                r.Place = r.Place.TrimEnd('T');
                place = int.Parse(r.Place);
            }
            
            results.Add(new Result {Name = name, ClubName = r.Club, Place = place});
        }

        return results;
    }
}