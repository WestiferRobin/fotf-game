To update the `fotf-api` submodule in your `fotf-game` repository after making changes, follow these steps:

### 1. Commit Changes in `fotf-api` Repository
First, ensure that you commit and push your changes to the `fotf-api` repository:
```bash
# Navigate to the fotf-api directory
cd path/to/fotf-api

# Add changes
git add .

# Commit changes
git commit -m "Description of the changes"

# Push changes to the remote repository
git push origin main  # or the appropriate branch you're working on
```

### 2. Update the Submodule Reference in `fotf-game`
Now, update the `fotf-api` submodule reference in your `fotf-game` project:
```bash
# Navigate to your fotf-game repository
cd path/to/fotf-game

# Update the submodule to the latest commit from the fotf-api repository
git submodule update --remote Assets/Scripts/fotf-api

# Stage the submodule reference change
git add Assets/Scripts/fotf-api

# Commit the update
git commit -m "Updated fotf-api submodule reference to latest commit"

# Push the changes to the fotf-game repository
git push origin main  # or the appropriate branch
```

### Explanation:
- **`git submodule update --remote`**: This command fetches the latest commit from the `fotf-api` repository and updates the submodule in your `fotf-game` project.
- **Committing the submodule**: The submodule reference is stored as a commit hash in `fotf-game`. Committing this change updates the pointer in the `fotf-game` repository to the new commit in `fotf-api`.

Now, your `fotf-game` repository will use the latest changes from the `fotf-api` submodule.