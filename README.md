#Description of the problem 



![image](https://github.com/marko1997221/GeneticAlgorithm/assets/61901835/4285811e-3598-46d0-8db2-35f27581a660)

The optimization problem is presented in figure no. 1. It is necessary to drill 263 holes on the printed circuit board shown in picture no. 1. Each hole has its own unique X and Y coordinates. A tile manufacturer is looking for an increase in the number of tiles produced per day. This result is achieved by reducing the length of the hole that the drill bit has to travel to drill 263 holes. This problem belongs to the TSP class of problems. Within the .txt file are the coordinates of the points to be loaded.

The optimization algorithm used to solve this problem is the "Genetic Algorithm". 

The formation of the first generation is realized by a random combination of 263 points, until the first population of 1000 elements is created. After which the optimization function is calculated. When creating the next generation, a process of selecting the best parents is performed, followed by random crossing between the selected parents until a population size of 1000 children is reached. At each crossing, the mutation probability of a particular child is checked. If the probability is higher than a certain one, the process of mutation occurs, which ends the cycle of creating a new generation. During the creation of each next generation, the results of the optimization functions are placed in the global variable, where the smallest value for the duration of this algorithm is sought. This process is repeated until the desired number of populations. The case that gives the best solution is realized after 2000 generations.
