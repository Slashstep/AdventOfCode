def searchMVP():
    topThree=[]
    
    with open('input.txt', 'r') as f:
        lines = f.readlines()
    
    currentCalSum = 0
    maxCalSum = 0
    
    for l in lines:
        if(l == "\n"):
            topThree.append(currentCalSum)
            currentCalSum = 0
        else:
            currentCalSum = currentCalSum + int(l)
    
    topThree.sort(reverse=True)
    
    sum = 0
    for i in range(0,3,1):
        sum = sum + topThree[i]

    print(sum)

searchMVP()