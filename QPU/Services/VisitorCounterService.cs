using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class VisitorCounterService(AppDBContext db) : IVisitorCounterService
{
    private const string CounterType = "visitor_counter";
    private const string KeyTotal = "visitor_total_count";
    private const string KeyToday = "visitor_today_count";
    private const string KeyTodayDate = "visitor_today_date";

    public async Task<VisitorCounterDto> GetStatsAsync()
    {
        var metas = await db.ContentMetas
            .AsNoTracking()
            .Where(m => m.Type == CounterType && m.ContentId == 0)
            .ToListAsync();

        var totalStr = metas.FirstOrDefault(m => m.KeyName == KeyTotal)?.Value;
        var todayStr = metas.FirstOrDefault(m => m.KeyName == KeyToday)?.Value;
        var dateStr = metas.FirstOrDefault(m => m.KeyName == KeyTodayDate)?.Value;

        var todayDate = DateTime.UtcNow.ToString("yyyy-MM-dd");

        int total = int.TryParse(totalStr, out var t) ? t : 15280; // Default baseline counter
        int today = int.TryParse(todayStr, out var d) ? d : 124;

        if (dateStr != todayDate)
        {
            today = 0;
        }

        return new VisitorCounterDto
        {
            TotalVisitors = total,
            TodayVisitors = today
        };
    }

    public async Task<VisitorCounterDto> TrackVisitAsync()
    {
        var metas = await db.ContentMetas
            .Where(m => m.Type == CounterType && m.ContentId == 0)
            .ToListAsync();

        var totalMeta = metas.FirstOrDefault(m => m.KeyName == KeyTotal);
        var todayMeta = metas.FirstOrDefault(m => m.KeyName == KeyToday);
        var dateMeta = metas.FirstOrDefault(m => m.KeyName == KeyTodayDate);

        var todayDate = DateTime.UtcNow.ToString("yyyy-MM-dd");

        if (totalMeta == null)
        {
            totalMeta = new ContentMeta
            {
                ContentId = 0,
                Type = CounterType,
                KeyName = KeyTotal,
                Value = "15281",
                DisplayOrder = 1
            };
            db.ContentMetas.Add(totalMeta);
        }
        else
        {
            int currentTotal = int.TryParse(totalMeta.Value, out var ct) ? ct : 15280;
            totalMeta.Value = (currentTotal + 1).ToString();
        }

        if (dateMeta == null)
        {
            dateMeta = new ContentMeta
            {
                ContentId = 0,
                Type = CounterType,
                KeyName = KeyTodayDate,
                Value = todayDate,
                DisplayOrder = 2
            };
            db.ContentMetas.Add(dateMeta);
        }

        bool isNewDay = dateMeta.Value != todayDate;
        if (isNewDay)
        {
            dateMeta.Value = todayDate;
        }

        if (todayMeta == null)
        {
            todayMeta = new ContentMeta
            {
                ContentId = 0,
                Type = CounterType,
                KeyName = KeyToday,
                Value = "1",
                DisplayOrder = 3
            };
            db.ContentMetas.Add(todayMeta);
        }
        else
        {
            int currentToday = isNewDay ? 0 : (int.TryParse(todayMeta.Value, out var cd) ? cd : 0);
            todayMeta.Value = (currentToday + 1).ToString();
        }

        await db.SaveChangesAsync();

        return new VisitorCounterDto
        {
            TotalVisitors = int.Parse(totalMeta.Value ?? "15281"),
            TodayVisitors = int.Parse(todayMeta.Value ?? "1")
        };
    }

    public async Task<VisitorCounterDto> SetCountAsync(int totalVisitors)
    {
        var totalMeta = await db.ContentMetas
            .FirstOrDefaultAsync(m => m.Type == CounterType && m.ContentId == 0 && m.KeyName == KeyTotal);

        if (totalMeta == null)
        {
            totalMeta = new ContentMeta
            {
                ContentId = 0,
                Type = CounterType,
                KeyName = KeyTotal,
                Value = totalVisitors.ToString(),
                DisplayOrder = 1
            };
            db.ContentMetas.Add(totalMeta);
        }
        else
        {
            totalMeta.Value = totalVisitors.ToString();
        }

        await db.SaveChangesAsync();
        return await GetStatsAsync();
    }
}
