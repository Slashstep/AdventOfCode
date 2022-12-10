# each backpack two large compartments
# each line has to be split in to two parts
# one letter is inside of both strings
# lower case has a - z = 1 - 26
# upper case has A - Z = 27 - 52
# Sum of all the doubled items. 


def doubleItemPerString():
    with open('input.txt', 'r') as f:
        lines = f.readlines()

    
    doubleString = ""
    # Split string into two strings and search for doubles
    for l in lines:
        leftLine = l[:len(l)//2]
        rightLine = l[len(l)//2:]

        doubleFound = False
        for i in range(0, len(leftLine), 1):
            for j in range(0, len(rightLine), 1):
                if(leftLine[i] == rightLine[j]):
                    doubleString = doubleString + leftLine[i]
                    doubleFound = True
                    break
                
            if(doubleFound == True):
                break
    calculateStringSum(doubleString)

def doubleItemPerGroup():
    with open('input.txt', 'r') as f:
        lines = f.readlines()


    trippleString = ""
    for h in range(0, len(lines)-2, 3):
        line1 = lines[h]
        line2 = lines[h+1]
        line3 = lines[h+2]

        doublesOfTwoStrings = ""
        for i in range(0, len(line1), 1):
            for j in range(0, len(line2), 1):
                if(line1[i] == line2[j]):
                    doublesOfTwoStrings = doublesOfTwoStrings + line1[i]
                    break

        doubleFound = False
        for i in range(0, len(line3), 1):
            for j in range(0, len(doublesOfTwoStrings), 1):
                if(line3[i] == doublesOfTwoStrings[j]):
                    trippleString = trippleString + line3[i]
                    doubleFound = True
                    break
        
            if(doubleFound == True):
                break

        
    
    calculateStringSum(trippleString)

    
def calculateStringSum(liste):
    # Calculate doubleSum
    sum = 0
    for c in liste:

        if ord(c) >= 65 and ord(c) <= 90:
            sum = sum + ord(c) - 38
        else:
            sum = sum + ord(c) - 96

    print(sum)

# doubleItemPerString()
doubleItemPerGroup()