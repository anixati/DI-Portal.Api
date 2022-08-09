using System.Collections.Generic;

namespace CrmDataExtractor
{
    public class Config
    {
        public static Dictionary<string, string> los = new Dictionary<string, string>
        {
            {"RoleAppointer","new_role|new_appointedby"},
            {"BoardStatus","new_board|new_boardactivestatus"},
            {"OwnerDivision","new_board|new_ownerdivisionoption"},
            {"OwnerPosition","new_board|new_ownerpositionoptions"},
            {"AppointmentStatus","new_appointment|new_appointmentstatus"},
            {"Capabilities","contact|new_capabilities"},
            {"Experience","contact|new_experience"},
            {"Title","contact|new_title"},

        };

        public static Dictionary<string, string> gos = new Dictionary<string, string>
        {

            {"Appointer","new_appointedbyoptionset"},
            {"AppointmentSource","new_appointmentsourceprocess"},
            {"EstablishedByUnder","new_establishedbyunder" },
            {"Judicial","new_judicial"},
            {"MinSubLocation","new_minsublocation"},
            {"Position","new_type"},
            {"RemunerationMethod","new_howisremunerationset"},
            {"RemunerationPeriod","new_remunerationperiod"},
            {"SelectionProcess","new_selectionprocess"},
            {"ReasonForNotReporting","new_reasonpositionnotgenderreportable"},
            {"Source","new_source"},
        };

        public static Dictionary<string, string> boards = new Dictionary<string, string>
        {
            {"Summary","new_actionpending"  },
            //{"PendingAction","new_actionpending"  },
            {"EstablishedByUnderText","new_establishedbyundermoreinfo"  },
            {"NominationCommittee","new_nominationcommittee"  },
            {"OwnerDivision","new_ownerdivision"  },
            {"OwnerPosition","new_ownerposition"  },
            {"LegislationReference","new_legislationreference"  },
            {"Constitution","new_constitution"  },
            {"QuorumRequiredText","new_quorumrequiredtext"  },
            {"OptimumMembers","new_optimummembers"  },
            {"MaximumTerms","new_maximumterms"  },
            {"MaximumMembers","new_maximummembers"  },
            {"MinimumMembers","new_minimummembers"  },
            {"QuorumRequired","new_quorumrequired"  },
            {"ReportingApproved","new_approvalcheck"  },
            {"ExcludeFromGenderBalance","new_excludefromgenderbalance"  },
            {"BoardStatus","new_boardstatus"  },
            {"EstablishedByUnder","new_establishedbyunder"  },
            {"ApprovedUser","new_approvedby"  },
            {"ResponsibleUser","new_responsibleofficeruser"  },
            {"AsstSecretary","new_responsibleas"  },
            {"AsstSecretaryPhone","new_responsibleasphone"  },
            {"MaxServicePeriod","new_maximumperiodofservice"  },
            {"Name","new_name"  },
            {"Description","new_description"  },

            {"Id","new_boardid"  },
            {"ApprovalCheck","new_approvalcheck"  },
            {"statuscode","statuscode"  },
            {"statecode","statecode"  },
          


        };
        public static Dictionary<string, string> boardRoles = new Dictionary<string, string>
        {
             {"Name",""  },
        {"BoardId",""  },
        {"Board",""  },
        {"IncumbentId",""  },
        {"Incumbent",""  },
        {"PositionId",""  },
        {"Position",""  },
        {"RoleAppointerId",""  },
        {"RoleAppointer",""  },
        {"IsFullTime",""  },
        {"IsExecutive",""  },
        {"IsExOfficio",""  },
        {"IsApsEmployee",""  },
        {"IsExNominated",""  },
        {"Term",""  },
        {"PositionRemunerated",""  },
        {"PaAmount",""  },
        {"RemunerationMethodId",""  },
        {"RemunerationMethod",""  },
        {"RemunerationTribunal",""  },
        {"VacantFromDate",""  },
        {"ExcludeFromOrder15",""  },
        {"ExcludeGenderReport",""  },
        {"IsSignAppointment",""  },
        {"NextSteps",""  },
        {"InstrumentLink",""  },
        {"PDMSNumber",""  },
        {"MinSubLocationId",""  },
        {"MinSubLocation",""  },
        {"MinisterOfficeDate",""  },
        {"MinisterActionDate",""  },
        {"LetterToPmDateType",""  },
        {"LetterToPmDate",""  },
        {"ExCoDateType",""  },
        {"ExCoDate",""  },
        {"NotifyLetterDateType",""  },
        {"NotifyLetterDate",""  },
        {"CabinetDateType",""  },
        {"CabinetDate",""  },
        {"InternalNotes",""  },
        {"ProcessStatus",""  },
        {"LeadTimeToAppoint",""  },
        {"MinSubDateType",""  },
        {"MinSubDate",""  },
        {"CreatedOn",""  },
        {"CreatedBy",""  },
        {"ModifiedOn",""  },
        {"ModifiedBy",""  },
        {"Id",""  },
        {"Locked",""  },
        {"Disabled",""  },
        {"Deleted",""  },
        {"Timestamp",""  },
        {"IsTransient",""  },
        };
    }

}
