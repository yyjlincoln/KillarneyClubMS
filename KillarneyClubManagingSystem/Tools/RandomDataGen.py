import random
Default = '''
$ General:Events
Long Jump
Javelin
200m
Discus
1500m

$ General:SortingMethods
Long Jump|largest
Javelin|smallest
200m|smallest
Discus|largest
1500m|smallest

$ General:PlacingToPoints
1|3
2|2
3|1

# =====================================
#                       AUTOMATICALLY GENERATED
#                            BY RANDOMDATAGEN.
# =====================================

'''

T = ''''''

with open('Names.txt','r') as f:
    T = f.read()

Q = '''Long Jump
Javelin
200m
Discus
1500m'''

Q = Q.split('\n')

T = T.split('\n')

Pool = ''

def pool(content=''):
    global Pool
    Pool+=content+'\n'

def pour():
    with open('data.ini','w') as f:
        f.write(Default+'\n'+Pool)


def init():
    global T
    quantity = int(input('How many names do you want? >'))
    T = T[:quantity]

    pool('$ Data:AtheleteNames')
    for x in T:
        pool(x)

    for x in T:
        pool('$ Data:Athelete:'+x)
        for y in Q:
            pool(f'{y}|{str(random.randint(1,10**7)*0.01)}')
        pool()
    pour()
    print('Random data generated.')
    print('Replace data.ini to load it to the program.')
    input('[Exit] >')
 
init()