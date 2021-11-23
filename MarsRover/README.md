# Mars Rover

## Types Of Input

### Starting Location For Rover

Correct Format = location:1,1,s   (xcoordinate, ycoordinate, direction facing)

Default starting location = 1,1,n

### List of Commands

Correct Format = commands:r,l,f,f,b

Possible commands include: 
- r = turn right
- l = turn left
- f = move forward
- b = move back
- s = shoot laser

Default commands = r,f,f,r,f,f,l,b

### List of Obstacles 

Correct Format = obstacles:1,1;2,3;3,3

Put coordinates for the obstacle with x,y coordinates.

Seperate the obstacles in the list with ;

If no obstacles are entered they will be randomly generated on the surface.

### Mode

Correct format = mode:destroyer

Possible modes include:
- explore
- map
- destroyer

Default mode = Destroyer

### Max Distance

Correct format = maxdistance:100

This is the furtherest distance travelled by the Rover before mission completes.

Default value = 100.

### Grid size

Correct format = gridsize:20

This is the size of the grid displayed on the screen.

Default value = 20.

### Loading From default CSV file

Correct format = csvfile

This will load the input from the default csv file.

### Loading from default JSON file

Correct format = jsonfile

This will load the input from the default json file.

### Loading from other external file

Correct format = filepath:(insert filepath)

The only files accepted are json and csv.