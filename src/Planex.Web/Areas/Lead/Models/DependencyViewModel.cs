﻿using Kendo.Mvc.UI;

public class DependencyViewModel : IGanttDependency
{
    public int DependencyID { get; set; }

    public int PredecessorID { get; set; }
    public int SuccessorID { get; set; }
    public DependencyType Type { get; set; }

//    public GanttDependency ToEntity()
//    {
//        return new GanttDependency
//        {
//            ID = DependencyID,
//            PredecessorID = PredecessorID,
//            SuccessorID = SuccessorID,
//            Type = (int)Type
//        };
//    }
}