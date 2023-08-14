using System.ComponentModel.DataAnnotations;

namespace scope_project_2.Models
{
    public class edit

    {
       
        public string? F_N { set; get; }


        public string? L_N { set; get; }


        public string? gender { set; get; }

        public string? DOB { set; get; }

        [MaxLength(100)]
        public string? Email { set; get; }

        public string? Phone { set; get; }

        public string? Country { set; get; }

        public string? State { set; get; }

        public string? city { set; get; }

        public string Read { set; get; }
        public string Play { set; get; }
        public string Cook { set; get; }



    }
}
