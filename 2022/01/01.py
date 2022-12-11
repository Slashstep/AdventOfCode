# Part 1: 69795, Part 2: 208437

def searchMVP(elvesToLookAt):
    topThree=[]
    
    with open('input.txt', 'r') as f:
        lines = f.readlines()
    
    currentCalSum = 0
    
    for l in lines:
        if(l == "\n"):
            topThree.append(currentCalSum)
            currentCalSum = 0
        else:
            currentCalSum += int(l)
    
    topThree.sort(reverse=True)
    
    sum = 0
    for i in range(0,elvesToLookAt,1):
        sum += topThree[i]

    print(sum)

searchMVP(1)    # Part 1
searchMVP(3)    # Part 2