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

$ Data:AtheleteNames
'''

with open('data.ini','w') as f:
    f.write(Default)

print('A default configuration file has been generated.')
print('Please copy that file to the same folder as the program.')
input('[Exit]>')