def detectFourDiffs():
    with open('input.txt', 'r') as f:
        lines = f.readlines()

    c1=""
    c2=""
    c3=""
    c4=""   
    
    for i in range(4, len(lines[0]), 1):
        c1=lines[0][i-3]
        c2=lines[0][i-2]
        c3=lines[0][i-1]
        c4=lines[0][i]

        if c1 != c2 and c1 != c3 and c1 != c4 and c2 != c3 and c2 != c4 and c3 != c4:
            print(i + 1)
            break

def detectFourteenDiffs():
    with open('input.txt', 'r') as f:
        lines = f.readlines()

    c = []
    
    for i in range(13, len(lines[0]), 1):
        #Befüllen
        for l in range(0, 14, 1):
            c.append(lines[0][i-l])

        #Checken
        if len(set(c)) == len(c):
            print(i+1)

        c.clear()

        

detectFourteenDiffs()
