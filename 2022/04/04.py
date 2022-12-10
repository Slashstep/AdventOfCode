# Anzahl der Paare finden, bei denen ein Teil den anderen vollständig einschließt

def overlappingPairs():
    with open('input.txt', 'r') as f:
        lines = f.readlines()

    
    doubleString = ""
    counter = 0
    # Split string into two strings and search for doubles
    for l in lines:
        leftLine, rightLine = l.split(sep=",", maxsplit=1)
        x1, x2 = leftLine.split(sep="-", maxsplit=1)
        y1, y2 = rightLine.split(sep="-", maxsplit=1)

        if int(x1) <= int(y1) and int(x2) >= int(y2):
            counter = counter + 1
        elif int(y1) <= int(x1) and int(y2) >= int(x2):
            counter = counter + 1
        
    print(counter)

def partiallyOverlappingPairs():
    with open('input.txt', 'r') as f:
        lines = f.readlines()

    
    doubleString = ""
    counter = 0
    # Split string into two strings and search for doubles
    for l in lines:
        leftLine, rightLine = l.split(sep=",", maxsplit=1)
        x1, x2 = leftLine.split(sep="-", maxsplit=1)
        y1, y2 = rightLine.split(sep="-", maxsplit=1)

        if int(x2) < int(y1):
            counter = counter + 1
        elif int(y2) < int(x1):
            counter = counter + 1
        
    print(1000 - counter)

#overlappingPairs()
partiallyOverlappingPairs()