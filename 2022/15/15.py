# Part 1: x < 4424278 Part 2: ####
# distress beacon pos 0 < xy < 4.000.000
# tuning freq x*4000000+y

lines = []
sensoryCor = []
beaconCor = []
sbDistances = []
lineCoverage = []

def inputToArray():
    global lines
    with open("input.txt", "r") as f:
        lines = f.readlines()

def parseInputToCoordinates(arr):
    for l in arr:
        split = l.split(sep=" ")
        
        sensoryCor.append((int(split[2][2:-1:]), int(split[3][2:-1:])))
        beaconCor.append((int(split[8][2:-1:]), int(split[9][2:-1:])))

def calculateSensorBeaconDistance(sensors, beacons):
    for i, sensor in enumerate(sensors):
        dist = abs(sensor[0]-beacons[i][0]) + abs(sensor[1] - beacons[i][1])
        sbDistances.append(dist)

def calculateLineCovarage(line, sensors, distances):
    for i in range(0, len(distances), 1):
        yDist = abs(sensors[i][1]-line)
        if distances[i] >= yDist:
            restDist = abs(distances[i] - yDist)
            for l in range(-int(restDist), int(restDist)+1, 1):
                lineCoverage.append(sensors[i][0]+l)
    
    return len(set(lineCoverage))-1

def calculateFreePosOnLine(line, sensors, distances):
    for i in range(0, len(distances), 1):
        yDist = abs(sensors[i][1]-line)
        if distances[i] >= yDist:
            restDist = abs(distances[i] - yDist)
            if sensors[i][0] - restDist < 0:
                leftX = 0
            else:
                leftX = -restDist
            if sensors[i][0] + restDist > 4000000:
                rightX = 4000000
            else:
                rightX = restDist
            for l in range(leftX , rightX, 1):
                lineCoverage.append(sensors[i][0]+l)
    
    return len(set(lineCoverage))-1

def findDistressPosition():
    for i in range(0, 4000000, 1):
        print(calculateFreePosOnLine(i, sensoryCor, sbDistances))
            


inputToArray()
parseInputToCoordinates(lines)
calculateSensorBeaconDistance(sensoryCor, beaconCor)
#print(calculateLineCovarage(2000000, sensoryCor, sbDistances))
findDistressPosition()
