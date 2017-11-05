new Vue({
  el: '#app',
  data: {
    todos: [],
    input: {
      description: ''
    },
    state: {
      addingTodo: false,
      clearingTodos: false
    }
  },
  created () {
    this.fetchTodos();
  },
  computed: {
    completed() {
      return this.todos.filter(todo => todo.isCompleted).length;
    },
    percentageCompleted() {
      const total = this.todos.length;

      return this.completed / total * 100;
    }
  },
  filters: {
    percentage(value) {
      return Math.round(value) + '%';
    }
  },
  methods: {
    addTodo(event) {
      event.preventDefault();

      const description = this.input.description.trim();

      if (!description || this.state.addingTodo) {
        return;
      }

      const body = JSON.stringify(description);
      const headers = new Headers();

      headers.append('Content-Type', 'application/json');

      this.state.addingTodo = true;

      return fetch('/api/todos', { method: 'POST', body, headers })
        .then(() => {
          this.input.description = '';
          this.state.addingTodo = false;
        })
        .then(() => this.fetchTodos());
    },
    toggleTodo(id) {
      fetch(`/api/todos/toggle/${id}`, { method: 'PUT' })
        .then(() => this.fetchTodos());
    },
    fetchTodos() {
      fetch('/api/todos')
        .then(res => res.json())
        .then(todos => {
          this.todos = todos;
        });
    },
    clearTodos() {
      if (this.state.clearingTodos) {
        return;
      }

      this.state.clearingTodos = true;

      fetch('/api/todos/clear', { method: 'DELETE' })
        .then(() => {
          this.state.clearingTodos = false;
        })
        .then(() => this.fetchTodos());
    }
  }
});
