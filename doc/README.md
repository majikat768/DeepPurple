# GIT Basics / getting started
## Install git from the official git website:
https://git-scm.com/downloads

## to clone from github:
Using a command terminal, navigate to the local directory you will be working in.
Then, run the following command:
```
$ git clone https://github.com/atki7828/DeepPurple
```
This will download the entire repository to your machine, in a directory called DeepPurple.

## git workflow
After you've cloned the repository, you can open the project folder "DeepPurple" in the Unity editor to begin working.
before starting work, be sure you have all of the most recent updates from the remote repository by using this command:
```
$ git pull
```
this will download any changes made to the repository since the last time you've pulled, and now it's safe to begin work without conflict.

After you've made changes you want to officially submit, use the following commands to push it to the remote repository:
```
$ git add .
$ git commit -m "message about your new update"
$ git push -u origin master
```

## More
More comprehensive guides to using git can be found here:
https://guides.github.com/
https://help.github.com/
