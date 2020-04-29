using ApplicationCore.DTOs;
using ApplicationCore.Models.ResourceRepresentations;
using ApplicationCore.Services;
using NewPlace.ResourceRepresentations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Converters
{
    public static class RepresentationConverters
    {
        public async static Task<AdvertisementRepresentation> ToRepresentation(this AdvertisementDto dto, string requestPath, IAdvertisementService service)
        {
            if (dto?.Id == null)
                throw new ArgumentNullException(nameof(dto));

            var thumbnailImage = await service.GetThumbnailBase64(dto.Id.Value);

            return new AdvertisementRepresentation()
            {
                Resource = dto,
                Thumbnail = thumbnailImage == null ? null : new ImageRepresentation()
                {
                    Resource = thumbnailImage,
                    MediaType = MediaTypeNames.Image.Jpeg,                                          // TODO: CheckMediaType(string64base)
                },
                Links = new List<Link>
                    {
                        new Link()
                        {
                            Rel = "self",
                            Href = Path.Combine("/", requestPath, dto.Id.ToString()!),
                            Method = HttpMethod.Get.ToString(),
                            Title = LinkDescriptions.Self
                        }
                    }
            };
        }
    }
}
