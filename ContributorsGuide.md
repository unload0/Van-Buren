# Contributor's Guide 📝

## Downloads

### Install Unity

Make sure you have **Unity Hub** installed, you can download it from [here](https://unity.com/download)

The project is using **Unity Version (6000.1.17f)**, you can download it from [here](https://unity.com/releases/editor/archive)

<img width="845" height="43" alt="image" src="https://github.com/user-attachments/assets/ee74148c-d2da-4858-9c01-5be52450aaba" />

### Node.js

You will also need Node.js for the project, you can get it [here](https://nodejs.org/en).

This will be used to automatically compress models when importing them into Unity to save storage.

Once you've finished installing Node.js, you need to also install **gltf-pipeline**

1. Open Command Prompt as administrator

![How to run Command Prompt cmdexe as administrator in Windows 10/11?](https://www.easyuefi.com/images/resource/type-cmd-in-windows-search-bar.png)

2. Enter the following command `npm install -g gltf-pipeline` then press enter
   
   <img width="551" height="144" alt="image" src="https://github.com/user-attachments/assets/af935220-21ff-45c4-bc4d-ab5dd89a3cf2" />

3. Done

### Github Desktop

Github Desktop will be used to work with the repository, you can download it from 
[here](https://desktop.github.com/download/)

once installed, launch the program and log in with you github account

you will then be able to clone the repository according to the image

<img width="564" height="129" alt="image" src="https://github.com/user-attachments/assets/9977956c-cd5a-4956-8049-8ab1726a33d5" />

you could either use the repo's Url https://github.com/unload0/Van-Buren.git or find it in the "Github.com" list.

## Workflow

This section will explain how to work on the project using the repository and Github Desktop.

Once you've installed everything and cloned the repo, you can open the unity project in Unity Hub by clicking "Add" then "Add project from disk" in the "Projects" menu.

Now that you have the Unity Project loaded, you can create a new branch on Github and name it according to what feature you want to implement.

<img width="419" height="344" alt="image" src="https://github.com/user-attachments/assets/7d65259c-55b3-4c0b-b93e-4a029ef662c4" />

You can create a new branch by cloning from the main branch or any other branch depending on what core functionalities you want to copy over.

<img width="624" height="350" alt="image" src="https://github.com/user-attachments/assets/b01e63a3-5eba-4e88-83b1-bfbf4838874e" />

Once you've successfully created a new branch, you can now switch over to your branch in Github Desktop by clicking on "Current Branch" dropdown next to "Fetch origin"

<img width="389" height="99" alt="image" src="https://github.com/user-attachments/assets/b7064884-5f70-4810-8a87-f9df501b6eff" />

always make sure that **you're not actively working on the main branch**

when you're done with your feature, you can then create a pull-request to the main branch.

<img width="370" height="555" alt="image" src="https://github.com/user-attachments/assets/fb704f50-6951-4b5c-bc6d-750252fefe75" />

**always make sure to build and check for errors before submitting a request!**



## Working on the computer Labs

When Opening the Unity Project in the labs computer, Unity Hub might tell you to install the correct version but **You don't have to install the specified version**, just select the option to open with Unity Version **6000.1.13f**.



### Node.js on Computer Lab

Computers at lab already have Node.js preinstalled, but you still need to do run the command `npm install -g gltf-pipeline` in command prompt.



### Install Github Desktop on Computer Lab

Installing Github Desktop directly **will not work** on the computer lab, **but there is a workaround:**

1. head to [this page](https://github.com/desktop/desktop/releases/latest) and download **GitHub Desktop 3.5.8 Windows x64 Full Nupkg**.

2. Once downloaded, right click the file and extract it either with 7zip or winrar.

3. When done extracting, enter the extracted folder then navigate to **lib -> net45**

4. You will find Github Desktop in that directory.
