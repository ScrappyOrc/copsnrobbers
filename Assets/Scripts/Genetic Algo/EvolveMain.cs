﻿using System;
using UnityEngine;
using System.Collections.Generic;

namespace ThreshEvolve
{
	class EvolveMain
	{
	   /* MainClass contains all the code that knows about the specific threshold
 		* being evolved. The program demos how the GA works by setting up a
 		* ThreshPop and simulating a generation (round of the game) using that
 		* population. The Gen2Phen method maps an int to whatever data type and
 		* range the threshold needs to be for its use in the game.  Actually, only
 		* the chromLeng lowest order bits in the int are manipulated by the GA
 		* machinery.  This means that the chromosome length is very likely way
 		* less than 32 bits and must be stricly <= 32 to fit in an unsigned int.
 		* The Fitness method simulates the fitness for an individual,
 		* which would be determined by the end of a round of the game (earlier for
 		* villagers that either make it on the cart or get eaten). This class only
 		* interacts directly with the ThreshPop class.
 		* 
 		* As a note, passing around a uint as the genotype instead of encapsulating
 		* it in an object is a bit lazy because it hard-wires a maximum 32-bit
 		* chromosome, but it simplifies the code because you can do arithmetic
 		* with it directly, and working with a chromosome some arbitrary length
 		* up to 32 bits gives plenty of space for high-resolution parameters.
 		* Actually, 8 or 10 bits might be plenty for most thresholds.
 		* Remember that the number of bits determines the size of the search space,
 		* so fewer bits will result in faster evolution, at the expense of precision
 		* in the parameter being evolved.
 		* Life is all about tradeoffs, even artificial life...
 		*/

		static int popSize = 20;				// Population size
		static int chromLeng = 3;              // Number of bits in a chromosome
		static int nChromVals = 1 << chromLeng; // Number of values for that many bits

		static List<Character> population;
		static ThreshPop tp;

		/// <summary>
		/// Initialize our algorithm for this population.
		/// </summary>
		/// <param name="population">The list of characters to evolve</param>
		public static void Initialize(GameObject[] pop)
		{
			popSize = pop.Length;

			// Extract the character component from each and store list of result
			List<Character> temp = new List<Character>();
			for (int i = 0; i < popSize; i++)
				temp.Add (pop[i].GetComponent<Character>());
			population = temp;

			// Create the population, either from the file or from scratch
			// Presumably the popSize would be the number of NPCs that will be
			// spawned for a round.  The data file name is set here as well by
			// passing it into the constructor.
			tp = new ThreshPop(chromLeng, popSize, "test1.txt");	//
			// TOMMY: 	Will need to use the chromosones of each character instead of
			//			randomly generated. Or will need to assign each character
			//			the randomly generated chromosome and alter it from there

			// This runs the initial fitness/chromosome generation/test
			EvolveMain.Main (null);
		}

		public static void Main (string[] args)
		{
			Debug.Log ("Running chromosome checkout and checkin+fitness");
			// Local storage for the chromosomes and fitness values to demonstrate
			// how the ThreshPop is used.  In this case, we'll just store an array
			// of chromosomes to represent the checked out population and manipulate
			// them in simple loops to make something happen.
			// In your game, a given threshold would be an attribute of an NPC,
			// and the fitness would be determined when that NPC is "done"
			uint [] chroms = new uint[popSize];
            // TOMMY: ^^^ Above, each member of the population will have a uint instead of an array

			// Check out all the individuals from the population to get their chroms
			// A CheckOut would be done when an NPC is spawned, one at a time
			int i = 0;
			while (! tp.AllCheckedOut())
			{
				chroms[i] = tp.CheckOut();
				// Store the proper chromosome in each character in our population
				population[i].chromosome = chroms[i];
				i++;
			}

			// Determine fitness values for everyone & check them all back into
			// the new population.
			// In the your game, this would happen one at a time as each NPC
			// is "done" and its fitness can be figured.
			// Note that the Individuals can be checked into the new population
			// in any order, likely not the order they were checked out.
			i = 0;
			while (! tp.AllCheckedIn())
			{
				//int fit = Fitness(Gen2Phen(chroms[i]));	// Determine fitness
				int fit = population[i].fitness;
				tp.CheckIn(chroms[i], fit);				// CheckIn to next generation
				i++;
			}

			// Save the new population for next time
			// This would be done at the end of each "round"
			tp.WritePop();
			// ^^^ This writes out the chromosome in uint form, and the fitness

			tp.DisplayPop(1);	// Display new population on the Console for grins
		}

		/* Maps genotype (chromLeng-bit chromosome) to phenotype (float).
		 * Phenotype could be any simple data type, but it's a float here
		 * with an arbitrary lower and upper bound that represent the smallest
		 * and largest possible values the threshold parameter can have in the
		 * game.  The mapping is a linear scaling of the nChromVals distinct values
		 * that are possible with the chromLeng-bit chromosome.  For a chromosome
		 * length of 10, this yields 1024 different values.  The mapping for your
		 * game could be anything you want: linear, non-linear, whatever.
		 */ 
		public static float Gen2Phen (uint gen, float lb, float ub)	// TOMMY - use to update threshold value in robber NOW
		{
			//float lb = 0.0f;			// Lower bound for threshold range in game
			//float ub = 200.0f;			// Upper bound
			float step = (ub - lb) / nChromVals;	// Step size for chrom values
			return (gen * step + lb);
		}
        // TOMMY: ^^^ 	Have to write this ourselves or could keep this but would have to stick within the
		//				fitness bounds that the default program presented.

		/* Generates a fitness value for a phenotype value
		 * Actual function is arbitrary, but needs to return an int,
		 * and it needs to be > 0 for roulette-wheel selection to work.
		 * With this in mind, make sure your fitness function returns
		 * integers > 0 for all possible cases.
		 * 
		 * For this example, the random element simulates the "noise" that
		 * happens in the environment, where better values "tend" to lead
		 * to higher survivability, but the correlation can be weak.
		 * By cranking up the random range and/or lowering the muliplier
		 * you can decrease the "signal to noise" ratio and explore how
		 * the GA behaves in noisy environments.  The values here lead to
 		 * signal-to-noise ratio of 1:1.  The function you build for your
 		 * game probably wouldn't have a random elemnent.
		 */
		static int Fitness (float phen)
		{
			return (int) (phen * 2 + Util.rand.Next(400));	// S/N = 1:1
            // TOMMY: ^^^ simulating a noisy fitness function. 
		}
        // TOMMY: ^^^ Have to write this ourselves to calculate a character's fitness based on...what?
	}

}
