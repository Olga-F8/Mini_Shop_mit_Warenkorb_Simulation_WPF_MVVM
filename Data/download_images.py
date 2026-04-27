import csv
import os
import requests

os.makedirs("images", exist_ok=True)

with open("products.csv", newline='', encoding="utf-8") as file:
    reader = csv.DictReader(file, delimiter=";")

    for row in reader:
        url = row["ImageUrl"]
        name = row["Name"].replace(" ", "_")
        path = f"images/{name}.jpg"

        try:
            img = requests.get(url).content
            with open(path, "wb") as f:
                f.write(img)
            print("Downloaded:", name)
        except:
            print("Failed:", name)