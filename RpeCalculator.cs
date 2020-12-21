using System;
using System.Collections.Generic;
using System.Linq;

public class TargetSet {
    public TargetSet(double targetWeight, int targetReps, double targetRpe){
        TargetWeight = targetWeight;
        TargetReps = targetReps;
        TargetRpe = targetRpe;
    }
    public double TargetWeight {get; private set;}
    public int TargetReps {get; private set;}
    public double TargetRpe {get; private set;}

    public override string ToString()
    {
        return $"{TargetWeight} x {TargetReps} @ {TargetRpe}";
    }
}

public class ResultItem {
    public ResultItem(double prevOneRepMax, double targetOneRepMax, List<TargetSet> targetSets){
        PrevOneRepMax = prevOneRepMax;
        TargetOneRepMax = targetOneRepMax;
        TargetSets = targetSets;
    }

    public double PrevOneRepMax{get;}
    public double TargetOneRepMax{get;}
    public List<TargetSet> TargetSets{get;}
}

public class RpeCalculator{
    private static Dictionary<int, Dictionary<double,double>> RpeChart = new Dictionary<int, Dictionary<double,double>> {
        {1,   new Dictionary<double,double>{ {10,100},  {9.5,97.8}, {9,95.5}, {8.5,93.9}, {8,92.9}, {7.5,90.7}, {7,89.2}, {6.5,87.8}, {6,86.3} }}, 
        {2,   new Dictionary<double,double>{ {10,95.5}, {9.5,93.9}, {9,92.2}, {8.5,90.7}, {8,89.2}, {7.5,87.8}, {7,86.3}, {6.5,85.0}, {6,83.7} }}, 
        {3,   new Dictionary<double,double>{ {10,92.2}, {9.5,90.7}, {9,89.2}, {8.5,87.8}, {8,86.3}, {7.5,85.0}, {7,83.7}, {6.5,82.4}, {6,81.1} }}, 
        {4,   new Dictionary<double,double>{ {10,89.2}, {9.5,87.8}, {9,86.3}, {8.5,85.0}, {8,83.7}, {7.5,82.4}, {7,81.1}, {6.5,79.9}, {6,78.6} }}, 
        {5,   new Dictionary<double,double>{ {10,86.3}, {9.5,85.0}, {9,83.7}, {8.5,82.4}, {8,81.1}, {7.5,79.9}, {7,78.6}, {6.5,77.4}, {6,76.2} }}, 
        {6,   new Dictionary<double,double>{ {10,83.7}, {9.5,82.4}, {9,81.1}, {8.5,79.9}, {8,78.6}, {7.5,77.4}, {7,76.2}, {6.5,75.1}, {6,73.9} }}, 
        {7,   new Dictionary<double,double>{ {10,81.1}, {9.5,79.9}, {9,78.6}, {8.5,77.4}, {8,76.2}, {7.5,75.1}, {7,73.9}, {6.5,72.3}, {6,70.7} }}, 
        {8,   new Dictionary<double,double>{ {10,78.6}, {9.5,77.4}, {9,76.2}, {8.5,75.1}, {8,73.9}, {7.5,72.3}, {7,70.7}, {6.5,69.4}, {6,68.0} }}, 
        {9,   new Dictionary<double,double>{ {10,76.2}, {9.5,75.1}, {9,73.9}, {8.5,72.3}, {8,70.7}, {7.5,69.4}, {7,68.0}, {6.5,66.7}, {6,65.3} }}, 
        {10,  new Dictionary<double,double>{ {10,73.9}, {9.5,72.3}, {9,70.7}, {8.5,69.4}, {8,68.0}, {7.5,66.7}, {7,65.3}, {6.5,64.0}, {6,62.6} }}, 
        {11,  new Dictionary<double,double>{ {10,70.7}, {9.5,69.4}, {9,68.0}, {8.5,66.7}, {8,65.3}, {7.5,64.0}, {7,62.6}, {6.5,61.3}, {6,59.9} }}, 
        {12,  new Dictionary<double,double>{ {10,68.0}, {9.5,66.7}, {9,65.3}, {8.5,64.0}, {8,62.6}, {7.5,61.3}, {7,59.9}, {6.5,58.6}, {6,57.4} }} };

    public static ResultItem Calculate(ExerciseItem exercise) {
        var prevIntensityFactor = (RpeChart[exercise.PreviousReps][exercise.PreviousRpe]) / 100.0;
        var prevE1rm = System.Math.Round(exercise.PreviousWeight / prevIntensityFactor);
        var targetE1rm = prevE1rm + (prevE1rm * (exercise.IncreasePercentage/100.0));    
        var targetSets = CreateTargetSets(targetE1rm, exercise.NextReps, new List<int>{6,7,8}, exercise.RpeSixOffsetPercentage);
        return new ResultItem(prevE1rm, targetE1rm, targetSets);
    }

    private static List<TargetSet> CreateTargetSets(double target1Rm, int targetReps, List<int>targetRpes, double rpeSixOffset) {
    var targetSets = new List<TargetSet>();
    
    foreach (var targetRpe in targetRpes) {
        Console.WriteLine($"Calculating for Target RPE of {targetRpe}");
        var nextIntensityFactor = (RpeChart[targetReps][targetRpe]) / 100.0;
        if(targetRpe >= 5.9 && targetRpe <= 6.1) {
            nextIntensityFactor -= (rpeSixOffset / 100.0);
        }
        var targetWeight = Math.Round(target1Rm * nextIntensityFactor);
        targetSets.Add(new TargetSet(targetWeight, targetReps, targetRpe));
        // console.log(`${targetWeight} x ${targetReps} @ ${targetRpe}`);
    }

    return targetSets;   
  }
}

