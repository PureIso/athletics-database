namespace AthleticsDatabase
{
    /// <summary>Contains useful global enumerations</summary>
    public static class Enumerations
    {
        #region Enumerations
        /// <summary>An enum of all the competition types</summary>
        public enum CompetitionType
        {
            /// <summary>All competition type - Indoor and Outdoor</summary>
            All = 0,
            /// <summary>Indoor Competition</summary>
            Indoor = 1,
            /// <summary>Outdoor Competitions</summary>
            Outdoor = 2
        }

        /// <summary>Athletes gender</summary>
        public enum Gender
        {
            /// <summary>Male</summary>
            Male = 1,
            /// <summary>Female</summary>
            Female = 2
        }

        /// <summary>Athletes region</summary>
        public enum Region
        {
            /// <summary>Leinster</summary>
            Leinster = 1,
            /// <summary>Ulster</summary>
            Ulster = 2,
            /// <summary>Munster</summary>
            Munster = 3,
            /// <summary>Connacht</summary>
            Connacht = 4,
            /// <summary>AllIreland</summary>
            AllIreland = 5,
            /// <summary>Non Irish </summary>
            NonIrish = 6
        }
        #endregion
    }
}
