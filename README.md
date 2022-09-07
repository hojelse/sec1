# sec1
Security 1 (Fall 2022)

## Setting up kali vagrant box and connecting with VSCode

requirements
- virtualbox
- vagrant

```
vagrant init kalilinux/rolling
vagrant up
```

```
vagrant ssh-config >> ~/.ssh/config
```

In vs code `ctrl+shift+p` and select "Remote-SSH: Connect to host"

Default credential for kali, user: `vagrant`, password: `vagrant`
