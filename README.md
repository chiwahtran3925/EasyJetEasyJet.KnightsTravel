# Project Overview
A program that represents a knight travelling through a chess board. It will mark the moves in sequence within the board of its travel. It has been built to search for all possible valid paths to fill the board. It will backtrack if it’s at a dead end.

This was built to demonstrate clean code and architecture.  Wherever possible, code is loosely coupled to ensure ease of testing, maintainability and flexibility.

The entry point is EasyJet.KnightsTravel.ConsoleApp

# Architecture & Design
## Layers / Structure
It has been structurally laid out architecturally for separation

- Application – EasyJet.KnightsTravel.Application - contains business logic, exceptions, validations, helper functionality
- Domain – EasyJet.KnightsTravel.Domain - interfaces and entities
- Infrastructure – EasyJet.KnightsTravel.Infrastructure - manipulation of the board and logging
- Presentation – EasyJet.KnightsTravel.Presentation - displaying the board 
- ConsoleApp – EasyJet.KnightsTravel.ConsoleApp - the console app where you can play the game
- Tests – EasyJet.KnightsTravel.Tests - tests the 3 main areas application, presentation and infrastructure.

## Design Choices
There are 4 main parts to this game:

- GameService – This is the entry point to start the game.
- PathFinderService – handles the recursion method to finding the paths. It was kept this way to keep the code simple.You could refactor  this into another service called BoardSolverService to handle the recursion.
- MovementService – gets a list of all valid potential moving points on a given spot on the board
- BoardService – marks, unmarks the board with a numbered path along with getting board information. This service can also be refactored into BoardWriter and BoardReader to distinguish between read and write methods.

# Algorithm
You first need to set the board up between 5 to 8. This is because anything under 5 will not match a full path. It has been restricted to 8 to help with performance and a chess board typically is 8x8. You then need to set a starting position within the board. Both of these inputs are validated on the console app to prevent incorrect inputs.

Once you start the game, the recursion will look for any valid positions on the board for a Knight. Which has a maximum movement of 8 different L shape. This will not always pick up 8, some of these spots could be outside the board or already been marked. 

For the below, X marks your position and the 8 possible movements.
```bash
. K . K . 
K . . . K
. . X . .
K . . . K
. K . K .
```
The start position will mark the board as 1. It will look for all valid points for this position. From these points, it will order by number of valid moves next. This was done to avoid running into dead ends and improve performance. It will loop to choose 1 of the valid points, and do the same recursion again. Up to when either the board completes (when all board points are set) or it hits a dead end. When it completes, it will exit all recursions and return. The recursion was chosen for its simplicity and clarity of moving within the board.

When there is a dead end, which is when it doesn’t find any valid positions. It will unmark the position and try the next position. This will continue until either all positions are tried or if there is a full solution.

# Testing
Tests are under EasyJet.KnightsTravel.Tests

I have used xUnit and Moq frameworks.

There are a mixture of unit test for specific test on services and functionality. There are also integration tests to check for the full game tests.

There is a GameServiceBuilder helper class to streamline the constructor setup.

## Testing Covers:

Functional
- Core logic
	- GameService 
	- PathFinderService 
	- MovementService 
	- BoardService 
- Validators
- Full tests
- Visualiser

Non Functional
- Exceptions
	- Board Size
	- Position validation
- Edge cases
	- Min/Max boards sizes
	- Invalid positions
	- Empty positions
	- Edge positions

# How to Run
Requirements: .NET 8.0 SDK.

```bash
dotnet run --project EasyJet.KnightsTravel.ConsoleApp
dotnet test
```

For Visual Studio users:

- Open EasyJet.KnightsTravel.sln
- Right-click EasyJet.KnightsTravel.ConsoleApp > "Set as Startup Project" > F5

For VS Code users:

- Open folder > open terminal > then run the above CLI commands.

### Note on Repository Naming
The repository name includes `EasyJetEasyJet.KnightsTravel` due to the initial setup, but the solution and namespaces are consistently named `EasyJet.KnightsTravel`.

# Example Output
```bash
Enter board size between 5-8:
5
Enter position X between 0 and 4:
0
Enter position Y between 0 and 4:
0
Board: 5X5
Starting position: (0,0)
Full travel for Knight completed
1       6       15      10      21
14      9       20      5       16
19      2       7       22      11
8       13      24      17      4
25      18      3       12      23
Do you want to play again? (Yes/No):
n
```
# Future Improvements
Configurable board sizes beyond 8. 8x8 runs fast but larger boards can take longer.

Smarter logging (structured logging instead of a console output).

Update recursion method to improve performance

# Key Learnings
Cleaner architecture applied to the problems.

Recursive problem solving in C#.

Balancing simplicity with extensibility.
