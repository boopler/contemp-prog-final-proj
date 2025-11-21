using System;
using System.ComponentModel.DataAnnotations;

namespace FavoriteDinnerApi.Models
{
    /// <summary>
    /// Represents a student's favourite dinner.  
    /// Contains at least five properties as required by the project rubric.
    /// </summary>
    public class FavoriteDinner
    {
        /// <summary>
        /// Primary key for the table.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The name of the student who enjoys this dinner.
        /// </summary>
        [Required]
        public string StudentName { get; set; }

        /// <summary>
        /// The name of the dish.
        /// </summary>
        [Required]
        public string DishName { get; set; }

        /// <summary>
        /// The cuisine type (e.g. Italian, Middle Eastern).
        /// </summary>
        [Required]
        public string CuisineType { get; set; }

        /// <summary>
        /// Indicates whether the dinner is cooked at home.
        /// </summary>
        public bool IsHomeCooked { get; set; }

        /// <summary>
        /// A rating between 1 and 10 indicating how much the student enjoys this dinner.
        /// </summary>
        [Range(1, 10)]
        public int RatingOutOf10 { get; set; }
    }
}