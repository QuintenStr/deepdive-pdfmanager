using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    /// <summary>
    /// DTO for input required to confirm participation in an excursion.
    /// </summary>
    public class ParticipationConfirmationInputDto
    {
        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public CoordinatesDto Coordinates { get; set; }

        [Required]
        public string QrCodeKey { get; set; }
    }

    /// <summary>
    /// Represents geographic coordinates.
    /// </summary>
    public class CoordinatesDto
    {
        [Required]
        public double Lat { get; set; }

        [Required]
        public double Long { get; set; }
    }
}
