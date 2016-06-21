using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class MissionViewModel
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDone { get; set; }
    }

    public class MissionsViewModel
    {
        public List<MissionViewModel> Missions;
        public List<MissionViewModel> DoneMissions;
        public int CountMissions;
        public int CountDoneMissions;
    }
}