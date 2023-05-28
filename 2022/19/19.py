# Part 1: ####, Part 2: ####
# ID*geoden
# Do this for every blueprint and add their numbers together

lines = []
blueprints = []

ore = 0
clay = 0
obsedian = 0
geode = 0

oreBot = 1
clayBot = 0
obsedianBot = 0
geodeBot = 0

def InputToArray():
    global lines
    with open("input.txt", "r") as f:
        lines = f.readlines()

def ParseInputToCoordinates():
    global blueprints
    for l in lines:
        split = l.split()
        blueprints.append((int(split[6]), int(split[12]), int(split[18]), int(split[21]), int(split[27]), int(split[30])))

def RiseOfTheRobots(time):
    ore = 0
    clay = 0
    obsedian = 0
    geode = 0

    oreBot = 1
    clayBot = 0
    obsedianBot = 0
    geodeBot = 0

    counter = 0

    for b in blueprints:
        for l in range(time):
            ore += oreBot
            clay += clayBot
            obsedian += obsedianBot
            geode += geodeBot

            if ore == b[4] and obsedian == b[5]:
                ore -= b[4]
                obsedian -= b[5]
                geodeBot += 1
            elif ore == b[2] and clay == b[3]:
                ore -= b[2]
                clay -= b[3]
                obsedianBot += 1
            elif ore == b[1]:
                ore -= b[1]
                clayBot += 1
            elif ore == b[0]:
                ore -= b[0]
                oreBot += 1

        print(b)
        print(ore)
        print(clay)
        print(obsedian)
        print(geode)
        print()


InputToArray()
ParseInputToCoordinates()
RiseOfTheRobots(24)