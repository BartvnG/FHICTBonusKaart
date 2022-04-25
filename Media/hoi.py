from flask import Flask, render_template
from flask import request

app = Flask(__name__)

@app.route("/home/")
def index():
    name = request.args["name"]
    file = open('test.txt')
    content = file.readlines()

    for i in range(len(content)):
        line = content[i]
        data = line.split(' ')
        if data[0]== name and name != "":
            streakT = int(data[2])
            daysT = 0
            puntenM = round((int(data[2])+5)/10)
            while (streakT%10) != 0:
                streakT += 1
                daysT +=1
                puntenT = round((streakT+5)/10+1)
            

            return render_template('index.html', puntenN=data[1], streakN = data[2],streakT = streakT, daysT = daysT, puntenT=puntenT , puntenM = puntenM, name=name) 

    else:
        return render_template("inlog.html")


    return render_template('index.html', kunst=data[1], kunst2 = data[2], name=name)

@app.route("/")
def login():
  return render_template("inlog.html")

if __name__ == "__main__":
    app.run()






    