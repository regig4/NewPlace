using System.Net.Mime;
using AdvertisementService.ApplicationCore.Application.Services;
using ApplicationCore.DTOs;
using Common.Dto.ResourceRepresentations;
using NewPlace.ResourceRepresentations;

namespace Infrastructure.Converters
{
    public static class RepresentationConverters
    {
        public static async Task<AdvertisementRepresentation> ToRepresentation(this AdvertisementDto dto, string requestPath, IAdvertisementApplicationService service)
        {
            if (dto?.Id == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            string? thumbnailImage = await service.GetThumbnailBase64(dto.Id.Value);

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
