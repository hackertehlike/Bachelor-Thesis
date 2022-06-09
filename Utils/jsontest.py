import os
import json
from tabnanny import check

# Read json file and save it to a dictionary
def read_json(filename):
    with open(filename) as f:
        data = json.load(f)
    return data

# Check if a text file contains an input string
# Return True if the string is found, False otherwise
def check_text(filename, string):
    with open(filename) as f:
        for line in f:
            if string in line:
                return True
    return False   

house = read_json('1ab527c3-ee58-4ec9-a37b-97411e1f84b5.json')

# initialize empty list
furniture = []

for item in house['furniture']:
    furniture.append(item['jid'])
    
print(furniture)

texture_not_found = []

# for each entry in furniture, copy the directory with the same name into the new directory "house_furniture"
for item in furniture:
    print(item)
    if(check_text("list_textures.txt", item)):
        print("Found")
        os.system("cp -r ~/../../Volumes/T7/3D-Future-model/" + item + " ~/../../Volumes/T7/house_furniture")
    else:
        print("Not found")
        texture_not_found.append(item)