using System.Data;
using ApplicationCore.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;

namespace RecommendationService.Data
{
    public class RecommendationsBasedOnLocationDao
    {
        private RecommendationsBasedOnLocationDao() { }

        public static RecommendationsBasedOnLocationDao Instance { get; } = new RecommendationsBasedOnLocationDao();


        public async Task<List<AdvertisementDetailsDto>> GetAdvertisementsInRadius(double latitude, double longitude, double radius)
        {
            using IDbConnection db = new SqlConnection(Configuration.DefaultConnectionString);
            IEnumerable<AdvertisementDetailsDto>? results = await db.QueryAsync<AdvertisementDetailsDto>(
                @"
                      select ad.id, title, description, apartment_type, pricing_type, create_date, valid_to, [login] user_name, apartment_type estate_type, 
                        area estate_area, city  estate_city, latitude, longitude, price, provision
					    from advertisement ad 
					    join estate e on e.id = ad.estate_id
					    join  Category c on c.id = ad.category_id 
					    join [user] u on u.id = ad.user_id
					    join [location] l on l.id = e.location_id 
                      where 6371 * acos(sin(@ltd) * sin(latitude) + cos(@ltd) * cos(latitude) * cos(longitude - @lng)) < @rad
                ", param: new { ltd = latitude, lng = longitude, rad = radius });
            return results.ToList();
        }
    }
}
