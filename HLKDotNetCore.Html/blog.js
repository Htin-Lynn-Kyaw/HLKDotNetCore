const tblBlog = "blogs";
let blogID = null;
GetBlogTabel();
//CreateBlog();

function Readblog() {
    console.log(GetBlogs());
}

function EditBlog(id) {
    let lst = GetBlogs();

    const item = lst.filter(x => x.id === id);

    if (item.length == 0) {
        console.log("no data found");
        Message("no data found", "error");
        return;
    }
    //return item[0];
    bindControls(item[0]);
}

function CreateBlog(title, author, content) {
    let lst = GetBlogs();

    const reqModel = {
        id: uuidv4(),
        title: title,
        author: author,
        content: content
    };

    lst.push(reqModel);
    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog);

    clearControls();

    Message("Saving successful.", "success");
}

function UpdateBlog(id, title, author, content) {
    let lst = GetBlogs();

    const item = lst.filter(x => x.id === id);

    if (item.length == 0) {
        console.log("no data found");
        return;
    }

    const anItem = item[0];
    anItem.title = title;
    anItem.author = author;
    anItem.content = content;

    const index = lst.findIndex(x => x.id === id);
    lst[index] = anItem;

    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog);

    clearControls();

    Message("Update successful.", "success");
}

function DeleteBlog(id) {
    confirmMessage("Are you sure?").then(
        function (value) {
            let lst = GetBlogs();

            const item = lst.filter(x => x.id === id);

            if (item.length == 0) {
                console.log("no data found");
                return;
            }

            lst = lst.filter(x => x.id !== id);

            const jsonBlog = JSON.stringify(lst);
            localStorage.setItem(tblBlog, jsonBlog);

            Message("Delete successful", "success");

            GetBlogTabel();
        }
    );
    /*Swal.fire({
        title: "Comfirm",
        text: "Are you sure?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes"
    }).then((result) => {
        if (!result.isConfirmed) return;
        let lst = GetBlogs();

        const item = lst.filter(x => x.id === id);

        if (item.length == 0) {
            console.log("no data found");
            return;
        }

        lst = lst.filter(x => x.id !== id);

        const jsonBlog = JSON.stringify(lst);
        localStorage.setItem(tblBlog, jsonBlog);

        Message("Delete successful", "success");

        GetBlogTabel();

    });*/
}

function GetBlogs() {
    const blogs = localStorage.getItem(tblBlog);

    let lst = [];
    if (blogs !== null) {
        lst = JSON.parse(blogs);
    }
    return lst;
}

function clearControls() {
    $('#txtTitle').val('');
    $('#txtAuthor').val('');
    $('#txtContent').val('');
    $('#txtTitle').focus();
}

function bindControls(item) {
    blogID = item.id;
    $('#txtTitle').val(item.title);
    $('#txtAuthor').val(item.author);
    $('#txtContent').val(item.content);
    $('#txtTitle').focus();
}

$('#btnSave').click(function () {
    const title = $('#txtTitle').val();
    const author = $('#txtAuthor').val();
    const content = $('#txtContent').val();

    if (blogID === null) {
        CreateBlog(title, author, content);
    } else {
        UpdateBlog(blogID, title, author, content);
        blogID = null;
    }

    GetBlogTabel();
})

function GetBlogTabel() {
    const lst = GetBlogs();
    let count = 0;
    let htmlRows = '';

    lst.forEach(x => {
        const htmlRow = ` 
        <tr>
            <td>${++count}</td>
            <td>${x.title}</td>
            <td>${x.author}</td>
            <td>${x.content}</td>
            <td>
                <button type="button" class="btn btn-primary" onclick="EditBlog('${x.id}')">Edit</button> |
                <button type="button" class="btn btn-danger" onclick="DeleteBlog('${x.id}')">Delete</button>
            </td>
        </tr>
        `;

        htmlRows += htmlRow;
    });

    $("#tbody").html(htmlRows);
}
