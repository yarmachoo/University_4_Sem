import matplotlib
import matplotlib.pyplot as plt
matplotlib.use('TkAgg')  # Or any other backend you prefer

# Read data from file
data_file = "logSMTOnData.txt"  # Replace with the path to your data file
with open(data_file, "r") as file:
    lines = file.readlines()

# Extract x and y data
x_data = []
y_data = []
for line in lines:
    parts = line.split()
    if len(parts) == 2:
        x_data.append(float(parts[0]))
        y_data.append(float(parts[1]))

# Combine and sort data
combined_data = sorted(zip(x_data, y_data))
sorted_x_data, sorted_y_data = zip(*combined_data)

# Plotting as bar chart
plt.figure(figsize=(10, 6))
plt.bar(sorted_x_data, sorted_y_data, color='green')
plt.title('AllCore Hyperthreading on')
plt.xlabel('Number of iterations')
plt.ylabel('Time in ns')
plt.grid(True)
plt.show()
import matplotlib
import matplotlib.pyplot as plt
matplotlib.use('TkAgg')  # Or any other backend you prefer

# Read data from file
data_file = "logSMTOffData.txt"  # Replace with the path to your data file
with open(data_file, "r") as file:
    lines = file.readlines()

# Extract x and y data
x_data = []
y_data = []
for line in lines:
    parts = line.split()
    if len(parts) == 2:
        x_data.append(float(parts[0]))
        y_data.append(float(parts[1]))

# Combine and sort data
combined_data = sorted(zip(x_data, y_data))
sorted_x_data, sorted_y_data = zip(*combined_data)

# Plotting as bar chart
plt.figure(figsize=(10, 6))
plt.bar(sorted_x_data, sorted_y_data, color='green')
plt.title('AllCore Hyperthreading Off')
plt.xlabel('Number of iterations')
plt.ylabel('Time in ns')
plt.grid(True)
plt.show()