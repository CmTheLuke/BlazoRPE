public class ExerciseSet {
    
}

public class ExerciseItem {
    public string Name{get; set;}
    public double PreviousWeight{get; set;}
    public int PreviousReps{get; set;}
    public double PreviousRpe{get; set;} = 8.0;
    public double IncreasePercentage{get; set;}
    public int NextReps{get; set;}  
    public double RpeSixOffsetPercentage{get; set;} = 5;

    public string RandomText {get;set;} = "This is totes rando!";  
}