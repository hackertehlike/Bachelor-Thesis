import os
import json

# Read json file and save it to a dictionary
def read_json(filename):
    with open(filename) as f:
        data = json.load(f)
    return data

house = read_json('0a9c667d-033d-448c-b17c-dc55e6d3c386.json')

# initialize empty list
furniture = []

for item in house['furniture']:
    furniture.append(item['jid'])
    

print(furniture)

# for each entry in furniture, copy the directory with the same name into the new directory "house_furniture"
for item in furniture:
    print(item)
    # retrieve the directory with the same name from "/T7/3D-Future-model" and copy it into the new directory "house_furniture"
    os.system("cp -r 3D-FUTURE-model/" + item + " ~/house_furniture2")
