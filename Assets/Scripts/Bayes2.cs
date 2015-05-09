using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public struct Observation 
{
	public int escapeIndex;
	public int citizens;
	public int cops;
	public bool rob;
}

public class Bayes2 
{
	public bool DEBUG = true;

	// Calculate the constant once
	private readonly double sqrt2PI = Math.Sqrt(2.0 * Math.PI);
	
	// List of observations.  Initialized from the data file
	// Added to with new observations during program run
	private List<Observation> obsTab = new List<Observation> ();

	// Value tables
	int[,] escapeCondition;
	double[,] escapeProportion;

	int[] citizenSum = new int[2];
	double[] citizenMean = new double[2];
	double[] citizenStdDev = new double[2];
	int[] citizenSumSq = new int[2];

	int[] copSum = new int[2];
	double[] copMean = new double[2];
	double[] copStdDev = new double[2];
	int[] copSumSq = new int[2];

	int[] robCondition = new int[2];
	double[] robProportion = new double[2];

	public void Initialize() 
	{
		escapeCondition = new int[City.escapes.Length, 2];
		escapeProportion = new double[City.escapes.Length, 2];

		for (int i = 0; i < 10; i++) {
			Observation initial;
			initial.citizens = i + 1;
			initial.cops = i / 5 + 1;
			initial.escapeIndex = i % City.escapes.Length;
			initial.rob = true;
			obsTab.Add (initial);
			Build();
		}
	}

	public void Add(Observation obs)
	{
		obs.citizens++;
		obs.cops++;
		obsTab.Add (obs);
		Build ();
	}

	public void Build() 
	{
		// Clear old data
		for (int i = 0; i < City.escapes.Length; i++) {
			escapeCondition [i, 0] = escapeCondition [i, 1] = 0;
		}
		citizenSum [0] = citizenSum [1] = 0;
		citizenSumSq [0] = citizenSumSq [1] = 0;
		copSum [0] = copSum [1] = 0;
		copSumSq [0] = copSumSq [1] = 0;
		robCondition [0] = robCondition [1] = 0;

		// Total up new data
		foreach (Observation obs in obsTab) 
		{
			int robOff = obs.rob ? 0 : 1;

			escapeCondition[(int)obs.escapeIndex, robOff]++;

			citizenSum[robOff] += obs.citizens;
			citizenSumSq[robOff] += obs.citizens * obs.citizens;

			copSum[robOff] += obs.cops;
			copSumSq[robOff] += obs.cops * obs.cops;

			robCondition[robOff]++;
		}

		// Calculate statistics
		CalcProps (escapeCondition, robCondition, escapeProportion);

		citizenMean [0] = Mean (citizenSum [0], robCondition [0]);
		citizenMean [1] = Mean (citizenSum [1], robCondition [1]);
		citizenStdDev [0] = StdDev (citizenSumSq [0], citizenSum [0], robCondition [0]);
		citizenStdDev [1] = StdDev (citizenSumSq [1], citizenSum [1], robCondition [1]);

		copMean [0] = Mean (copSum [0], robCondition [0]);
		copMean [1] = Mean (copSum [1], robCondition [1]);
		copStdDev [0] = StdDev (copSumSq [0], copSum [0], robCondition [0]);
		copStdDev [1] = StdDev (copSumSq [1], copSum [1], robCondition [1]);

		robProportion [0] = (double)robCondition [0] / obsTab.Count;
		robProportion [1] = (double)robCondition [1] / obsTab.Count;

		DumpStats ();
	}

	/******************************************************/
	// Standard statistical functions.  These should be useful without modification.
	
	// Calculates the proportions for a discrete table of counts
	// Handles the 0-frequency problem by assigning an artificially
	// low value that is still greater than 0.
	public void CalcProps (int[,] counts, int[] n, double[,] props)
	{
		for (int i = 0; i < counts.GetLength(0); i++) {
			for (int j = 0; j < counts.GetLength(1); j++) {
				// Detects and corrects a 0 count by assigning a proportion
				// that is 1/10 the size of a proportion for a count of 1
				if (counts [i, j] == 0) {
			
					if (robCondition [j] == 0)
						props [i, j] = 0.1d;
					else 
						props [i, j] = 0.1d / robCondition [j];	// Can't have 0
				} else
					props [i, j] = (double)counts [i, j] / n [j];
			}
		}
	}
	
	public double Mean (int sum, int n)
	{
		if (n == 0)
			return sum;
		return (double)sum / n;
	}
	
	public double StdDev(int sumSq, int sum, int n)
	{
		if (n < 2)
			return 1;
		return Math.Sqrt((sumSq - (sum*sum)/(double)n) / (n-1));
	}
	
	// Calculates probability of x in a normal distribution of
	// mean and stdDev.  This corrects a mistake in the pseudo-code,
	// used a power function instead of an exponential.
	public double GauProb (double mean, double stdDev, int x)
	{
		double xMinusMean = x - mean;
		return (1.0d / (stdDev*sqrt2PI)) * 
			Math.Exp(-1.0d*xMinusMean*xMinusMean / (2.0d*stdDev*stdDev));
	}
	
	/*********************************************************/

	public bool ShouldRob(int escapeIndex, int citizens, int cops) 
	{
		citizens++;
		cops++;

		return CalcBayes (escapeIndex, citizens, cops, true) > 
			CalcBayes (escapeIndex, citizens, cops, false);
	}

	// Bayes likelihood for four condition values and one action value
	// For each possible action value, call this with a specific set of four
	// condition values, and pick the action that returns the highest
	// likelihood as the most likely action to take, given the conditions.
	public double CalcBayes(int escapeIndex, int citizens, int cops, bool rob)
	{
		int playOff = rob ? 0 : 1;
		double like = escapeProportion[escapeIndex, playOff] *
			GauProb(citizenMean[playOff], citizenStdDev[playOff], citizens) *
				GauProb(copMean[playOff], copStdDev[playOff], cops) *
				robProportion[playOff];

		if (DEBUG) {
			Debug.Log("Params[" + escapeIndex + ", " + citizens + ", " + cops + ", " + rob + "] Yields " + like);
		}

		return like;
	}

	// Dump all the statistics to the Console for debugging purposes
	public void DumpStats()
	{
		using (StreamWriter write = new StreamWriter("dump.txt")) 
		{
			write.WriteLine ("Escapes ");
			for (int i = 0; i < City.escapes.Length; i++)
				for (int j = 0; j < 2; j++)
					write.WriteLine (escapeCondition[i, j] + " " + escapeProportion[i, j] + " ");
			write.WriteLine ();
		
			write.WriteLine ("Citizens ");
			write.WriteLine (citizenSum [0] + " " + citizenSum [1] + " ");
			write.WriteLine (citizenSumSq [0] + " " + citizenSumSq [1] + " ");
			write.WriteLine (citizenMean [0] + " " + citizenMean [1] + " ");
			write.WriteLine (citizenStdDev [0] + " " + citizenStdDev [1]);
		
			write.WriteLine ("Cops ");
			write.WriteLine (copSum [0] + " " + copSum [1] + " ");
			write.WriteLine (copSumSq [0] + " " + copSumSq [1] + " ");
			write.WriteLine (copMean [0] + " " + copMean [1] + " ");
			write.WriteLine (copStdDev [0] + " " + copStdDev [1]);
	
			write.WriteLine ("Rob ");
			write.WriteLine (robCondition [0] + " " + robProportion [0] + " ");
			write.WriteLine (robCondition [1] + " " + robProportion [1]);

			write.WriteLine();
			write.WriteLine("-- Observations ---------------------");
			write.WriteLine();

			foreach (Observation obs in obsTab)
			{
				write.WriteLine("Entry:");
				write.WriteLine ("  Escape:   " + obs.escapeIndex);
				write.WriteLine ("  Citizens: " + obs.citizens);
				write.WriteLine ("  Cops:     " + obs.cops);
				write.WriteLine ("  Rob:      " + obs.rob);
			}
		}
	}
}
