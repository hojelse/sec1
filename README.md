# sec1
Security 1 (Fall 2022)

## Setting up kali vagrant box and connecting with VSCode

Requirements
- virtualbox
- vagrant

Launch kali
- Download kali box `vagrant init kalilinux/rolling`
- Launch kali box `vagrant up`

Default credential for kali, user: `vagrant`, password: `vagrant`

Setup VS Code SSH
- Add kali box ssh config to local config `vagrant ssh-config >> ~/.ssh/config`
- In vs code `ctrl+shift+p` and select "Remote-SSH: Connect to host"
g

## Notes

Set keyboard language to DK `setxkbmap -layout dk`